using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Capstone.Models;


namespace Capstone.DAO
{
    public class CardSqlDao : ICardDao
    {
        private readonly string connectionString;
        public CardSqlDao(string connString)
        {
            connectionString = connString;
        }

        public List<Card> GetMyCards(int userId)
        {
            List<Card> cardList = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT textcard_id, users.username, creator_id, front, back, card_keywords, " +
                        "date_made FROM textcard JOIN users ON creator_id = user_id WHERE user_id = @user_id;", conn);
                    cmd.Parameters.AddWithValue("@user_id", userId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    cardList = new List<Card>();
                    while (reader.Read())
                    {
                        cardList.Add(CreateCardFromReader(reader));
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.Error.WriteLine(ex.Message);
            }

            return cardList;
        }

        public List<Card> GetPublicCards()
        {
            List<Card> cardList = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT textcard.textcard_id, users.username, textcard.creator_id, front, back, card_keywords, " +
                        "textcard.date_made FROM textcard JOIN users ON creator_id = user_id JOIN deck_textcard ON " +
                        "deck_textcard.textcard_id = textcard.textcard_id " +
                        "JOIN deck ON deck.deck_id = deck_textcard.deck_id WHERE is_public = 1;", conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    cardList = new List<Card>();
                    while (reader.Read())
                    {
                        cardList.Add(CreateCardFromReader(reader));
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.Error.WriteLine(ex.Message);
            }

            return cardList;
        }

        public List<Card> GetMyCardsByKeywords(int userId, string cardKeyword)
        {
            List<Card> cardList = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string wildKeyword = "%" + cardKeyword + "%";
                    SqlCommand cmd = new SqlCommand("SELECT DISTINCT textcard_id, users.username, creator_id, front, back, card_keywords, " +
                        "date_made FROM textcard JOIN users ON creator_id = user_id WHERE card_keywords LIKE @wild_keyword AND creator_id = @user_id;", conn);
                    cmd.Parameters.AddWithValue("@wild_keyword", wildKeyword);
                    cmd.Parameters.AddWithValue("@user_id", userId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    cardList = new List<Card>();
                    while (reader.Read())
                    {
                        cardList.Add(CreateCardFromReader(reader));
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.Error.WriteLine(ex.Message);
            }

            return cardList;
        }

        public List<Card> GetPublicCardsByKeywords(string cardKeyword)
        {
            List<Card> cardList = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string wildKeyword = "%" + cardKeyword + "%";
                    SqlCommand cmd = new SqlCommand("SELECT DISTINCT textcard.textcard_id, users.username, textcard.creator_id, front, back, card_keywords, " +
                        "textcard.date_made FROM textcard JOIN users ON creator_id = user_id JOIN deck_textcard ON textcard.textcard_id = deck_textcard.textcard_id " +
                        "JOIN deck ON deck_textcard.deck_id = deck.deck_id " +
                        "WHERE card_keywords LIKE @wild_keyword AND is_public = 1;", conn);
                    cmd.Parameters.AddWithValue("@wild_keyword", wildKeyword);
                    SqlDataReader reader = cmd.ExecuteReader();
                    cardList = new List<Card>();
                    while (reader.Read())
                    {
                        cardList.Add(CreateCardFromReader(reader));
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.Error.WriteLine(ex.Message);
            }

            return cardList;
        }



        public List<Card> GetAllCardsByDeckId(int deckId)
        {
            List<Card> cardList = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT textcard.textcard_id, users.username, textcard.creator_id, front, back, card_keywords, " +
                        "textcard.date_made FROM textcard JOIN users ON creator_id = user_id JOIN deck_textcard ON " +
                        "textcard.textcard_id = deck_textcard.textcard_id WHERE deck_id = @deck_id", conn);
                    cmd.Parameters.AddWithValue("@deck_id", deckId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    cardList = new List<Card>();
                    while (reader.Read())
                    {
                        cardList.Add(CreateCardFromReader(reader));
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.Error.WriteLine(ex.Message);
            }

            return cardList;
        }

        public Card GetCard(int cardId)
        {
            // establish the SQL connection
            Card card = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT textcard_id, users.username, creator_id, front, back, card_keywords, " +
                        "date_made FROM textcard JOIN users ON creator_id = user_id WHERE textcard_id = @card_id;", conn);
                cmd.Parameters.AddWithValue("@card_id", cardId);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    card = CreateCardFromReader(reader); // create a card object from the information passed in by the database
                }
            }

            return card;
        }

        
        public Card CreateCard(Card card)
        {
            int newCardId;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("INSERT INTO textcard (front, back, card_keywords, " +
                                                "date_made, creator_id) " +
                                                "OUTPUT INSERTED.textcard_id " +
                                                "VALUES (@front, @back, @card_keywords, " +
                                                "@date_made, @creator_id);", conn);
                cmd.Parameters.AddWithValue("@front", card.Front);
                cmd.Parameters.AddWithValue("@back", card.Back);
                cmd.Parameters.AddWithValue("@card_keywords", card.CardKeywords);
                cmd.Parameters.AddWithValue("@creator_id", card.CreatorId);
                cmd.Parameters.AddWithValue("@date_made", DateTime.Today);


                newCardId = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return GetCard(newCardId);
        }

        

        public Card UpdateCard(int cardId, Card card)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("UPDATE textcard SET front = @front, back = @back, card_keywords = @card_keywords, " +
                    "date_made = @date_made, creator_id = @creator_id WHERE textcard_id = @textcard_id; ", conn);
                cmd.Parameters.AddWithValue("@date_made", card.DateMade);
                cmd.Parameters.AddWithValue("@card_id", cardId);
                cmd.Parameters.AddWithValue("@front", card.Front);
                cmd.Parameters.AddWithValue("@back", card.Back);
                cmd.Parameters.AddWithValue("@card_keywords", card.CardKeywords);
                cmd.Parameters.AddWithValue("@creator_id", card.CreatorId);
                cmd.Parameters.AddWithValue("@textcard_id", card.CardId);

                cmd.ExecuteNonQuery();
            }

            Card checkCard = GetCard(cardId);


            return checkCard;
        }

        public bool DeleteCard(int cardId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("DELETE FROM textcard WHERE textcard_id = @card_id;", conn);
                cmd.Parameters.AddWithValue("@card_id", cardId);
                        
                cmd.ExecuteNonQuery();
            }

            // If there is still information at the location of this deck, it was not deleted properly
            if (GetCard(cardId) != null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        

        private Card CreateCardFromReader(SqlDataReader reader)
        {
            Card card = new Card();
            card.Creator = Convert.ToString(reader["username"]);
            card.CreatorId = Convert.ToInt32(reader["creator_id"]);
            card.CardId = Convert.ToInt32(reader["textcard_id"]);
            card.Front = Convert.ToString(reader["front"]);
            card.Back = Convert.ToString(reader["back"]);
            card.CardKeywords = Convert.ToString(reader["card_keywords"]);
            card.DateMade = Convert.ToDateTime(reader["date_made"]);

            return card;
        }
    }

}
