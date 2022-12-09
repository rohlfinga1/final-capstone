using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models;
using System.Data.SqlClient;

namespace Capstone.DAO
{
    public class DeckSqlDao : IDeckDao
    {
        private readonly string connectionString;
        public DeckSqlDao(string connString)
        {
            connectionString = connString;
        }

        public List<Deck> GetAllDecks()
        {
            List<Deck> deckList = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM deck", conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    deckList = new List<Deck>();
                    while (reader.Read())
                    {
                        deckList.Add(CreateDeckFromReader(reader));
                    }
                }
            }
            catch(SqlException ex)
            {
                Console.Error.WriteLine(ex.Message);
            }

            return deckList;
        }

        public List<Deck> GetAllPublicDecks()
        {
            List<Deck> publicDeckList = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM deck WHERE is_public = 1;", conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    publicDeckList = new List<Deck>();
                    while (reader.Read())
                    {
                        publicDeckList.Add(CreateDeckFromReader(reader));
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.Error.WriteLine(ex.Message);
            }

            return publicDeckList;
        }

        public Deck GetDeck(int deckId)
        {
            // establish the SQL connection
            Deck deck = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM deck WHERE deck_id = @deck_id;", conn);
                    cmd.Parameters.AddWithValue("@deck_id", deckId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        deck = CreateDeckFromReader(reader); // create a deck object from the information passed in by the database
                    }
                }
            }
            catch(SqlException)
            {
                throw;
            }

            return deck;
        }

        //public List<Deck> GetAllDecksByCreatorId(int creatorId)
        //{
        //    List<Deck> deckList = null;

        //    try
        //    {
        //        using (SqlConnection conn = new SqlConnection(connectionString))
        //        {
        //            conn.Open();

        //            SqlCommand cmd = new SqlCommand("SELECT * FROM deck WHERE creator_id = @creator_id", conn);
        //            cmd.Parameters.AddWithValue("@creator_id", creatorId);
        //            SqlDataReader reader = cmd.ExecuteReader();
        //            deckList = new List<Deck>();
        //            while (reader.Read())
        //            {
        //                deckList.Add(CreateDeckFromReader(reader));
        //            }
        //        }
        //    }
        //    catch (SqlException ex)
        //    {
        //        Console.Error.WriteLine(ex.Message);
        //    }

        //    return deckList;
        //}

        //public Deck CreateDeck(string name, bool isPublic, string description, string deckKeywords)
        public Deck CreateDeck(Deck deck)
        {
            int newDeckId;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("INSERT INTO deck (name, description, " +
                                                "is_public, deck_keywords, creator_id) " +
                                                "OUTPUT INSERTED.deck_id " +
                                                "VALUES (@name, @description, " +
                                                "@is_public, @deck_keywords, @creator_id);", conn);
                
                cmd.Parameters.AddWithValue("@name", deck.Name);
                cmd.Parameters.AddWithValue("@description", deck.Description);
                cmd.Parameters.AddWithValue("@is_public", deck.IsPublic);
                cmd.Parameters.AddWithValue("@deck_keywords", deck.DeckKeywords);
                cmd.Parameters.AddWithValue("@creator_id", 1);
                


                newDeckId = Convert.ToInt32(cmd.ExecuteScalar());
            }
            Deck newDeck = GetDeck(newDeckId);

            return newDeck;
        }

        public Deck UpdateDeck(int deckId, Deck deck)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("UPDATE deck " +
                                                "SET creator_id = @creator_id, name = @name, description = @description, is_public = @is_public, deck_keywords = @deck_keywords " +
                                                "WHERE deck_id = @deck_id;", conn);
                cmd.Parameters.AddWithValue("@deck_id", deck.DeckId);
                cmd.Parameters.AddWithValue("@creator_id", deck.CreatorId);
                cmd.Parameters.AddWithValue("@name", deck.Name);
                cmd.Parameters.AddWithValue("@description", deck.Description);
                cmd.Parameters.AddWithValue("@is_public", deck.IsPublic);
                cmd.Parameters.AddWithValue("@deck_keywords", deck.DeckKeywords);

                cmd.ExecuteNonQuery();
            }

            Deck checkDeck = GetDeck(deckId);


            return checkDeck;
        }

        public bool DeleteDeck(int deckId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("DELETE FROM deck WHERE deck_id = @deck_id;", conn);
                cmd.Parameters.AddWithValue("@deck_id", deckId);

                cmd.ExecuteNonQuery();
            }

            // If there is still information at the location of this deck, it was not deleted properly
            if (GetDeck(deckId) != null)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        private Deck CreateDeckFromReader(SqlDataReader reader)
        {
            Deck deck = new Deck();
            deck.DeckId = Convert.ToInt32(reader["deck_id"]);
            deck.CreatorId = Convert.ToInt32(reader["creator_id"]);
            deck.Name = Convert.ToString(reader["name"]);
            deck.Description = Convert.ToString(reader["description"]);
            deck.IsPublic = Convert.ToBoolean(reader["is_public"]);
            deck.DeckKeywords = Convert.ToString(reader["deck_keywords"]);

            return deck;
        }

        public List<Deck> GetDecksForUser(int userId)
        {
            List<Deck> userDecks = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM deck WHERE creator_id = @userId;", conn);
                    cmd.Parameters.AddWithValue("@userId", userId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    userDecks = new List<Deck>();
                    while (reader.Read())
                    {
                        userDecks.Add(CreateDeckFromReader(reader));
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.Error.WriteLine(ex.Message);
            }

            return userDecks;
        }
    }
}
