using System.Collections.Generic;
using System.Data.SQLite;
using VPhotoLoader.Api;

namespace VPhotoLoader.SqLiteManager
{
    public class SQLiteProvider : ISqlProvider
    {
        private string _dbPath;

        public SQLiteProvider(string dbPath)
        {
            _dbPath = dbPath;
        }

        public static SQLiteProvider CreateDataBase(string path)
        {
            SQLiteConnection.CreateFile(path);
            using (SQLiteConnection conn = new SQLiteConnection(string.Format("Data Source={0};", path)))
            {
                conn.Open();
                string sql = "CREATE TABLE LoadedPhotos (ID INTEGER NOT NULL , AlbumID INTEGER NOT NULL , OwnerID INTEGER NOT NULL , Link TEXT)";
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
            return new SQLiteProvider(path);
        }

        public void Insert(IEnumerable<Photo> photos)
        {
            using (SQLiteConnection conn = new SQLiteConnection(string.Format("Data Source={0};", _dbPath)))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand(conn))
                {
                    using (var transaction = conn.BeginTransaction())
                    {
                        foreach (var photo in photos)
                        {
                            cmd.CommandText = "INSERT INTO LoadedPhotos (ID, AlbumID, OwnerID, Link) VALUES (@ID, @AlbumID, @OwnerID, @Link)";
                          
                            cmd.Parameters.AddWithValue("@ID", photo.ID);
                            cmd.Parameters.AddWithValue("@AlbumID", photo.AlbumID);
                            cmd.Parameters.AddWithValue("@OwnerID", photo.OwnerID);
                            cmd.Parameters.AddWithValue("@Link", photo.Link);
                            
                            cmd.ExecuteNonQuery();  
                        }
                        transaction.Commit();
                    }
                }
                conn.Close();
            }
        }

        public IEnumerable<Photo> GetPhotos(int ownerId, int albumId)
        {
            List<Photo> result = new List<Photo>();
            using (SQLiteConnection conn = new SQLiteConnection(string.Format("Data Source={0};", _dbPath)))
            {
                conn.Open();
                using (SQLiteCommand command = new SQLiteCommand(conn))
                {
                    command.CommandText = "SELECT * FROM LoadedPhotos WHERE OwnerID IS @OwnerID AND AlbumID IS @AlbumID";
                    
                    command.Parameters.AddWithValue("@OwnerID", ownerId);
                    command.Parameters.AddWithValue("@AlbumID", albumId);
                    
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new Photo(
                            reader.GetInt32(0),
                            reader.GetInt32(1),
                            reader.GetInt32(2),
                            reader.GetString(3)
                        ));
                    }
                }
                conn.Close();
            }
            return result;
        }
    }
}