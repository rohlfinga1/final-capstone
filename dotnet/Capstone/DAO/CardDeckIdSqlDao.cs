using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Capstone.Models;

namespace Capstone.DAO
{
    public class CardDeckIdSqlDao : ICardDeckIdDao
    {
        private readonly string connectionString;
        public CardDeckIdSqlDao(string connString)
        {
            connectionString = connString;
        }
        public List<CardDeckId> GetCardDeckIdArray()
        {
            // establish the SQL connection
            List<CardDeckId> cardDeckIdList = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM deck_textcard;", conn);
                    //cmd.Parameters.AddWithValue("@card_id", cardId);

                    SqlDataReader reader = cmd.ExecuteReader();
                    cardDeckIdList = new List<CardDeckId>();
                    while (reader.Read())
                    {
                        cardDeckIdList.Add(CreateCardDeckIdFromReader(reader)); // create a card object from the information passed in by the database
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
            return cardDeckIdList;
        }
        public bool AddCardToDeck(int deckId, int cardId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO deck_textcard (deck_id, textcard_id) VALUES(@deck_id, @textcard_id); ", conn);
                    cmd.Parameters.AddWithValue("@deck_id", deckId);
                    cmd.Parameters.AddWithValue("@textcard_id", cardId);

                    cmd.ExecuteNonQuery();
                }

            }
            catch (SqlException ex)
            {
                Console.Error.WriteLine(ex.Message);
                return false;
            }

            return true;
        }

        public bool DeleteCardFromDeck(int deckId, int cardId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("DELETE FROM deck_textcard WHERE deck_id = @deck_id AND textcard_id = @textcard_id;", conn);
                    cmd.Parameters.AddWithValue("@deck_id", deckId);
                    cmd.Parameters.AddWithValue("@textcard_id", cardId);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                Console.Error.WriteLine(ex.Message);
                return false;
            }
            return true;

        }

        private CardDeckId CreateCardDeckIdFromReader(SqlDataReader reader)
        {
            CardDeckId cardDeckId = new CardDeckId();
            cardDeckId.CardId = Convert.ToInt32(reader["textcard_id"]);
            cardDeckId.DeckId = Convert.ToInt32(reader["deck_id"]);

            return cardDeckId;
        }
    }
}
