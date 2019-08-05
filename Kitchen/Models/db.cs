using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Kitchen.Models
{

    public static class db
    {
        public static string GetUserName(int UserId)
        {
            DataTable dt = GetData(String.Format(@"Select UserName FROM UserProfile WHERE UserId = " + UserId));
            if (dt == null || dt.Rows.Count == 0) return null;

            return (string)dt.Rows[0]["UserName"].ToString();
        }

        public static bool IsInRole(string UserId, string RoleName)
        {
            if (UserId == "") return false;
            DataTable dt = GetData(String.Format(@"
                                    SELECT        webpages_Roles.RoleId, webpages_UsersInRoles.RoleId AS Expr1
                                    FROM            webpages_Roles INNER JOIN
                                                       webpages_UsersInRoles ON webpages_Roles.RoleId = webpages_UsersInRoles.RoleId
                                    WHERE         (webpages_UsersInRoles.UserId = N'{0}') 
                                    AND           (webpages_Roles.RoleName = N'{1}')",
                                    UserId,RoleName));
            if (dt != null && dt.Rows.Count > 0)
                return true;
            else
                return false;
        }

        public static DataTable GetData(string query)
        {
            DataTable result = new DataTable();

            string myConnString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            if (string.IsNullOrEmpty(myConnString)) return null;

            SqlConnection conn = new SqlConnection(myConnString);
            SqlCommand command = new SqlCommand(query, conn);

            conn.Open();
            //try
            //{
            SqlDataAdapter da = new SqlDataAdapter(command);
            // this will query your database and return the result to your datatable
            da.Fill(result);
            da.Dispose();
            conn.Close();
            //}
            //catch (Exception) { return null; }


            return result;
        }

        public static bool SetData(string query)
        {
            string myConnString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            if (string.IsNullOrEmpty(myConnString)) return false;

            SqlConnection conn = new SqlConnection(myConnString);
            SqlCommand command = new SqlCommand(query, conn);

            conn.Open();
            //try
            //{
            command.ExecuteNonQuery();
            conn.Close();
            //}
            //catch (Exception) { return null; }


            return true;
        }

        #region Kitchens

        public static Models.addKitchenModels GetThing(int id)
        {
            if (id == 0) return null;
            DataTable dt = GetData("select top 1 * from Album where id = " + id);
            if (dt==null || dt.Rows.Count == 0) return null;
            addKitchenModels result = new addKitchenModels((int)dt.Rows[0]["id"],(Enums.category)dt.Rows[0]["category"],(Enums.brand)dt.Rows[0]["brand"],dt.Rows[0]["article"].ToString(),  dt.Rows[0]["name"].ToString(), dt.Rows[0]["description"].ToString());
            return result;
        }

        public static Models.ShowKitchenModel GetThing(string article)
        {
            if (string.IsNullOrEmpty(article)) return null;
            DataTable dt = GetData(string.Format(@"
                SELECT        Album.id, Album.category, Album.brand, Album.article, Album.name, Album.description, Photo.image
                FROM            Album LEFT OUTER JOIN
                                         Photo ON Album.id = Photo.albumId
                WHERE        (Album.article = N'{0}')
                ORDER BY Photo.mainImg DESC
            ", article));
            if (dt.Rows.Count == 0) return null;
            ShowKitchenModel result = new ShowKitchenModel((int)dt.Rows[0]["id"], (Enums.category)dt.Rows[0]["category"], (Enums.brand)dt.Rows[0]["brand"], dt.Rows[0]["article"].ToString(), dt.Rows[0]["name"].ToString(), dt.Rows[0]["description"].ToString());
            foreach (DataRow dRow in dt.Rows)
            {
                if (dRow["Image"] != DBNull.Value) result.Images.Add((byte[])dRow["Image"]);
            }
            return result;
        }

        public static int SetThing(Models.addKitchenModels model)
        {
            return int.Parse(GetData(String.Format(@"Insert into Album (category, article, name, description, brand) VALUES({0},N'{1}',N'{2}',N'{3}',{4}) SELECT SCOPE_IDENTITY() AS [SCOPE_IDENTITY]", (int)model.Category, model.Article, model.Name, model.Description, (int)model.Brand)).Rows[0]["SCOPE_IDENTITY"].ToString());
            
            //return SetData(String.Format(@"Insert into Things (CategoryId, Article, Description, Features) VALUES({0},'{1}','{2}','{3}')",(int)model.Category,model.Article,model.Description,model.Features));
        }

        public static void UpdateThing(Models.addKitchenModels model)
        {
            var p1 = (int)model.Category;
            var p2 = model.Article;
            var p3 = model.Name;
            var p4 = model.Description;
            var p5 = (int)model.Brand;
            var p6 = model.Id;
            GetData(String.Format(@"Update Album set category = {0}, article = N'{1}', name = N'{2}', description = N'{3}', brand = {4} where id = {5}", p1, p2,p3, p4, p5, p6));
        }

        public static int addImg(int kitchen, byte[] buffer)
        {
            DataTable result = new DataTable();
            string myConnString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            if (string.IsNullOrEmpty(myConnString)) return 0;

            string queryString = String.Format(@"

            declare @count int
            SELECT @count = COUNT(id) FROM Photo where albumId = {0} and mainImg = 1;
            if @count>0
                Insert into Photo (albumId, image, mainImg) VALUES({0}, @img, 0)
            else
                Insert into Photo (albumId, image, mainImg)VALUES({0}, @img, 1) SELECT SCOPE_IDENTITY() AS [SCOPE_IDENTITY]", kitchen);

            SqlConnection conn = new SqlConnection(myConnString);
            SqlCommand command = new SqlCommand(queryString, conn);

            command.Parameters.Add("@img", SqlDbType.Binary);
            command.Parameters["@img"].Value = buffer;

            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(command);
            da.Fill(result);
            command.ExecuteNonQuery();
            conn.Close();
            return int.Parse(result.Rows[0]["SCOPE_IDENTITY"].ToString());
        }

        public static ShowKitchensList ShowKitchens(ref ShowKitchensList model)
        {
            model.listThing = new List<ShowKitchensList.strListThigs>();
            DataTable dt = GetData(string.Format(@"
                declare @myCount int;

                SELECT     @myCount = COUNT(id) 
                FROM            Album
                WHERE        (category = {1}) AND (brand = 0)


                SELECT        TOP ({0}) Album.name, Album.id, Album.article, Photo.Image, @myCount as countThings
                FROM            Album LEFT OUTER JOIN
                                         Photo ON Album.id = Photo.albumId  AND Photo.mainImg = 1
                WHERE        (Album.category= {1}) AND (Album.id NOT IN
                                             (SELECT        TOP ({2}) id
                                               FROM            Album AS Things_1))
            ", model.count, model.category, (model.page - 1) * model.count));

            if (dt != null && dt.Rows.Count > 0) model.countThings = (int)dt.Rows[0]["countThings"];

            if (dt != null && dt.Rows.Count > 0)
            foreach (DataRow dRow in dt.Rows)
            {
                model.listThing.Add(new ShowKitchensList.strListThigs(dRow["name"].ToString(), (int)dRow["id"], dRow["article"].ToString(), dRow["image"] == DBNull.Value ? null : (byte[])dRow["image"]));
            }
            return model;
        }

        #endregion

        #region Recipe
        public static int AddRecipe(RecipeModel model)
        {
            return int.Parse(GetData(String.Format(@"Insert into Recipe (name, ingredients, brief, description) VALUES(N'{0}',N'{1}',N'{2}',N'{3}')  SELECT SCOPE_IDENTITY() AS [SCOPE_IDENTITY]", model.Name, model.Ingridients, model.Brief, model.Description)).Rows[0]["SCOPE_IDENTITY"].ToString());
        }

        public static void UpdateRecipe(Models.RecipeModel model)
        {
            GetData(String.Format(@"Update Recipe set name = N'{0}',ingredients = N'{1}', brief = N'{2}', description = N'{3}' where id = {4}",model.Name, model.Ingridients, model.Brief, model.Description, model.Id));
        }

        public static RecipeModel GetRecipe(int id)
        {
            if (id == 0) return null;
            DataTable dt = GetData("select top 1 * from Recipe where id = " + id);
            if (dt==null || dt.Rows.Count == 0) return null;
            RecipeModel result = new RecipeModel((int)dt.Rows[0]["id"], dt.Rows[0]["name"].ToString(), dt.Rows[0]["ingredients"].ToString(),   dt.Rows[0]["brief"].ToString(),  dt.Rows[0]["description"].ToString(),   dt.Rows[0]["photo"] == DBNull.Value ? null : (byte[])dt.Rows[0]["photo"]);
            return result;
        }

        public static void setPhoto(int recipeId, byte[] buffer)
        {
            string myConnString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            if (string.IsNullOrEmpty(myConnString)) return;

            string queryString = String.Format(@"
                declare @count int
                SELECT @count = COUNT(id) FROM Recipe where id = {0}

                if  @count > 0
                Update Recipe set photo = @photo where id = {0}
                else
                Insert into Recipe (photo) VALUES (@photo)
            ", recipeId);

            SqlConnection conn = new SqlConnection(myConnString);
            SqlCommand command = new SqlCommand(queryString, conn);

            command.Parameters.Add("@photo", SqlDbType.Image);
            command.Parameters["@photo"].Value = buffer;

            conn.Open();
            command.ExecuteNonQuery();
            conn.Close();
        }

        public static List<Models.RecipeModel> GetRecipeList()
        {
            List<Models.RecipeModel> result = new List<Models.RecipeModel>();
            DataTable dt = GetData("select * from Recipe");
            if (dt != null)
            {
                foreach (DataRow dRow in dt.Rows)
                {
                    var brief = (string)dRow["brief"];
                    while (brief.IndexOf("<br") != -1)
                    {
                        int x = brief.IndexOf("<br");
                        brief = brief.Substring(0, x) + brief.Substring(x + 5);
                    }
                    if (brief.Length > 370)
                    {
                        brief = brief.Substring(0,370);
                        brief += "...";
                    }
                    result.Add(new RecipeModel((int)dRow["id"], (string)dRow["name"], (string)dRow["ingredients"], brief, (string)dRow["description"],  dRow["photo"] == DBNull.Value ? null : (byte[])dRow["photo"]));
                }
            }
            return result;
        }

        #endregion

        #region Albums

        public static int addAlbum(AlbumsModel model)
        {
            DataTable result = new DataTable();

            string myConnString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            if (string.IsNullOrEmpty(myConnString)) return 0;

            string queryString = String.Format(@"Insert into Album ( Position, Alias, Name, Description) 
                                                                VALUES({0}, '{1}', N'{2}', N'{3}') SELECT SCOPE_IDENTITY() AS [SCOPE_IDENTITY]",
                                                                model.Position, model.Alias, model.Name, model.Description == null ? "" : model.Description.Replace("'", "''"));


            SqlConnection conn = new SqlConnection(myConnString);
            SqlCommand command = new SqlCommand(queryString, conn);

            //try
            //{
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(command);
            da.Fill(result);
            conn.Close();
            //}
            //catch (Exception) {
            //    return -1;
            //}

            if (result.Rows.Count == 0) return 0;
            return int.Parse(result.Rows[0]["SCOPE_IDENTITY"].ToString());
        }

        public static List<AlbumsModel> ListAlbums()
        {
            DataTable dt = GetData("select * from Album WHERE category = 0");
            if (dt == null || dt.Rows.Count == 0) return null;
            List<AlbumsModel> result = new List<AlbumsModel>();
            foreach (DataRow dRow in dt.Rows)
            {
                result.Add(
                    new AlbumsModel((int)dRow["Id"], dRow["Alias"].ToString(), (int)dRow["Position"], dRow["Name"].ToString(), dRow["Description"].ToString(), dRow["ImgAlbum"] == DBNull.Value ? null : (byte[])dRow["ImgAlbum"], dRow["PermissionRole"].ToString())
                    );
            }
            return result;
        }

        public static bool addPhoto(int albumId, byte[] previewImg, byte[] Img, string descr, int UserName)
        {
            string myConnString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            if (string.IsNullOrEmpty(myConnString)) return false;

            string queryString = String.Format(@"
                IF (SELECT        COUNT(1) FROM            Photo
                WHERE        (AlbumId = {0})) = 0
                Update Album set ImgAlbum = @Img where (Id = {0})
               
                Insert into Photo ( AlbumId, PreviewImg, Image, Date, Description, UserId ) 
                VALUES({0}, @prevImg, @Img, @date, N'{1}', {2})
                ",
                albumId, descr == null ? "" : descr.Replace("'", "''"), UserName);

            SqlConnection conn = new SqlConnection(myConnString);
            SqlCommand command = new SqlCommand(queryString, conn);

            command.Parameters.Add("@date", SqlDbType.SmallDateTime);
            command.Parameters["@date"].Value = DateTime.Now;

            command.Parameters.Add("@prevImg", SqlDbType.Image);
            command.Parameters["@prevImg"].Value = previewImg;

            command.Parameters.Add("@Img", SqlDbType.Image);
            command.Parameters["@Img"].Value = Img;

            conn.Open();
            command.ExecuteNonQuery();
            conn.Close();

            return true;
        }

        public static PhotoModel showPhoto(int PhotoId, int UserId)
        {
            DataTable dt = GetData("select top 1 * from Photo where Id = " + PhotoId);
            if (dt == null || dt.Rows.Count == 0) return null;
            bool isCanVote = IsCanVote(PhotoId, UserId);
            var p1 = (int)dt.Rows[0]["Id"];
            var p2 = (int)dt.Rows[0]["AlbumId"];
            var p3 = (byte[])dt.Rows[0]["Image"];
            var p4 = (DateTime)dt.Rows[0]["Date"];
            var p5 = dt.Rows[0]["Description"].ToString();
            var p6 = (decimal)dt.Rows[0]["rating"];
            var p7 = (int)dt.Rows[0]["ratingCount"];
            var p8 = (bool)isCanVote;
            return new PhotoModel(p1, p2, p3, p4, p5, p6, p7, p8);
        }

        public static AlbumsModel showAlbum(string alias, bool withPhotos)
        {
            if (string.IsNullOrEmpty(alias)) return null;

            string query = "";
            int id = 0;
            if (int.TryParse(alias, out id))
            {
                query = string.Format(@"

                SELECT        Album.Id AS AlbumId, Album.Alias, Album.Position, Album.Name, Album.Description, Album.ImgAlbum, Album.PermissionRole, Photo.Id, Photo.PreviewImg
                FROM            Album LEFT OUTER JOIN
                                         Photo ON Album.Id = Photo.AlbumId
                WHERE        (Album.Id = {0})

                ", alias);
            }
            else
            {
                query = string.Format(@"

                SELECT        Album.Id AS AlbumId, Album.Alias, Album.Position, Album.Name, Album.Description, Album.ImgAlbum, Album.PermissionRole, Photo.Id, Photo.PreviewImg
                FROM            Album LEFT OUTER JOIN
                                         Photo ON Album.Id = Photo.AlbumId
                WHERE        (Album.Alias = N'{0}')

                ", alias);
            }
            DataTable dt = GetData(query);

            if (dt == null || dt.Rows.Count == 0) return null;
            AlbumsModel result = new AlbumsModel((int)dt.Rows[0]["AlbumId"], dt.Rows[0]["Alias"].ToString(), (int)dt.Rows[0]["Position"], dt.Rows[0]["Name"].ToString(), dt.Rows[0]["Description"].ToString(),
                dt.Rows[0]["ImgAlbum"] == DBNull.Value ? null : (byte[])dt.Rows[0]["ImgAlbum"], dt.Rows[0]["PermissionRole"].ToString());

            if (withPhotos)
            {
                result.PreviewPhotos = new List<EditPhotoModel>();
                foreach (DataRow dRow in dt.Rows)
                {
                    if (dRow["PreviewImg"] != DBNull.Value) result.PreviewPhotos.Add(new EditPhotoModel((int)dRow["Id"], (byte[])dRow["PreviewImg"]));
                }
            }

            return result;
        }

        public static void editAlbum(AlbumsModel model)
        {
            SetData(string.Format(@"
            Update Album set Alias = '{1}', Position = {2}, Name = N'{3}', Description = N'{4}', PermissionRole = '{5}' where Id = {0}
            ", model.AlbumId
            , model.Alias
            , model.Position
            , model.Name
            , model.Description
            , model.PermissionRole));
        }

        public static void deletePhoto(int id)
        {
            SetData(string.Format(@"
            delete from Photo where Id = {0}
            ", id));
        }

        public static AlbumsModel showAlbumForPhoto(int PhotoId)
        {
            if (PhotoId == 0) return null;
            DataTable dt = GetData(String.Format(@"
                SELECT        Id, Alias, Position, Name, Description, ImgAlbum, PermissionRole
                FROM            Album
                WHERE        (Id =
                                             (SELECT        Id
                                               FROM            Photo
                                               WHERE        (Id = {0})))
            ",PhotoId));

            if (dt == null || dt.Rows.Count == 0) return null;
            return new AlbumsModel((int)dt.Rows[0]["Id"], dt.Rows[0]["Alias"].ToString(), (int)dt.Rows[0]["Position"], dt.Rows[0]["Name"].ToString(), dt.Rows[0]["Description"].ToString(),
                dt.Rows[0]["ImgAlbum"] == DBNull.Value ? null : (byte[])dt.Rows[0]["ImgAlbum"], dt.Rows[0]["PermissionRole"].ToString());
        }




        #endregion

        #region Voting
        public static bool IsCanVote(int PhotoId, int UserId)
        {
            bool res = false;
            if (UserId != 0)
            {
                DataTable dt = GetData(string.Format("select top 1 * from Rating where photoId = {0} and userId = {1}", PhotoId, UserId));
                if (dt == null || dt.Rows.Count == 0)
                    res = true;
                else
                {
                    System.DateTime VoteDate = (System.DateTime)dt.Rows[0]["date"];
                    if (System.DateTime.Now.Date > VoteDate.Date) res = true;
                }
            }
            return res;
        }

        public static bool SetVote(int photoId, int UserId, decimal value)
        {
            if (IsCanVote(photoId, UserId))
            {
                SetData(String.Format("update Photo set rating = rating+{0} , ratingCount = ratingCount+1 WHERE Id = {1}", value.ToString().Replace(",","."), photoId));

                string myConnString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
                if (string.IsNullOrEmpty(myConnString)) return false;

                string queryString = String.Format(@"
                    IF (SELECT COUNT(1) FROM Rating WHERE userId = {0} AND photoId = {1}) = 0
                    Insert into Rating (userId,date,photoId) 
                    VALUES({0},@date,{1})
                    ELSE
                    Update Rating set date = @date WHERE userId = {0} AND photoId = {1}",
                    UserId, photoId);

                SqlConnection conn = new SqlConnection(myConnString);
                SqlCommand command = new SqlCommand(queryString, conn);

                command.Parameters.Add("@date", SqlDbType.SmallDateTime);
                command.Parameters["@date"].Value = DateTime.Now;
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
                return true;
            }
            else return false;

        }
        #endregion

        #region Zamer

        public static ZamerModels GetInfo(int userId)
        {
            DataTable dt = GetData("select top 1 * from UserProfile where UserId = " + userId);
            if (dt==null || dt.Rows.Count == 0) return null;
            ZamerModels result = new ZamerModels((int)dt.Rows[0]["UserId"], dt.Rows[0]["UserName"].ToString(), dt.Rows[0]["PhoneNumber"].ToString(), dt.Rows[0]["Address"].ToString());
            return result;
        }

        public static bool SetZakaz(ZamerModels model)
        {
            string myConnString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            if (string.IsNullOrEmpty(myConnString)) return false;

            string queryString = String.Format(String.Format(@"Insert into Zamer (UserName, Phone, Address, Time, UserId, Date, ManagerId, ManagerName, Status) VALUES(N'{0}', N'{1}', N'{2}', N'{3}', {4}, @date, 0, ' ', 1) SELECT SCOPE_IDENTITY() AS [SCOPE_IDENTITY]", (string)model.UserName, (string)model.Phone, (string)model.Address, (string)model.Time, (int)model.UserId));

            SqlConnection conn = new SqlConnection(myConnString);
            SqlCommand command = new SqlCommand(queryString, conn);

            command.Parameters.Add("@date", SqlDbType.DateTime);
            command.Parameters["@date"].Value = DateTime.Now;

            conn.Open();
            command.ExecuteNonQuery();
            conn.Close();

            return true;
        }
        #endregion

        #region Otziv

        public static void GetOtziv(ref OtzivList model)
        {
            model.otziv = new List<OtzivList.Otziv>();
            DataTable dt = GetData(string.Format(@"
           declare @myCount int;

                SELECT     @myCount = COUNT(Id) 
                FROM            Otziv
                WHERE           Otziv.IsShowed = 1
                
                SELECT        TOP ({0}) Id, UserId, UserName, Date, Description, IsShowed ,@myCount as countThings
                FROM            Otziv AS Otziv1
                WHERE        Otziv1.IsShowed = 1 And (Id NOT IN
                             (SELECT        TOP ({1}) Id
                               FROM            Otziv AS Otziv3))
                                          
            ", model.count,  (model.page-1)*model.count));

            if (dt != null && dt.Rows.Count > 0) model.countOtziv = (int)dt.Rows[0]["countThings"];

            if (dt != null && dt.Rows.Count > 0)
            foreach(DataRow dRow in dt.Rows)
            {
                model.otziv.Add(new OtzivList.Otziv((int)dRow["Id"],(string)dRow["UserName"].ToString(), (DateTime)dRow["Date"], (string)dRow["Description"].ToString(), (bool)dRow["IsShowed"]));
            }
        }

        public static void SendOtziv(string text, string UserId)
        {
            string myConnString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            string UserName = GetUserName(Int32.Parse(UserId));
            if (!string.IsNullOrEmpty(myConnString))
            {
                string queryString = String.Format(@"
                    Insert into Otziv 
                    Values ({0}, N'{1}', @date, N'{2}', 1)
                ", 1, UserName, text);

                SqlConnection conn = new SqlConnection(myConnString);
                SqlCommand command = new SqlCommand(queryString, conn);

                command.Parameters.Add("@date", SqlDbType.SmallDateTime);
                command.Parameters["@date"].Value = DateTime.Now;
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
            }
        }

        public static bool HideOtziv(int Id, int val)
        {
            return SetData(String.Format(@"
            Update Otziv 
            Set IsShowed = {1}
            Where Id = {0}",
               Id,val ));
        }
        #endregion

        #region Online_Raschet

        public static bool Set_Raschet(RaschetModels model)
        {
            string myConnString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            if (string.IsNullOrEmpty(myConnString)) return false;

            string queryString = String.Format(@"
                            INSERT INTO Online_Serv
                            (UserId, UserName,Type, Form, Wishes, Date, ManagerId, ManagerName, Status, Comments)
                           VALUES        ({0}, N'{4}',{1}, {2}, N'{3}', @date, 0, ' ', 1, ' ')"
                , model.UserId, (int)model.Type, model.Form, model.Text, GetUserName(model.UserId));

            SqlConnection conn = new SqlConnection(myConnString);
            SqlCommand command = new SqlCommand(queryString, conn);

            command.Parameters.Add("@date", SqlDbType.DateTime);
            command.Parameters["@date"].Value = DateTime.Now;

            conn.Open();
            command.ExecuteNonQuery();
            conn.Close();

            return true;
        }

        #endregion

        #region Manager

        public static int GetZakazCount(int table, int status)
        {
            DataTable dt = GetData(String.Format(@"Select * From {0} WHERE Status = {1}", Models.Enums.GetZakazType(table), status));
            return dt.Rows.Count;
        }

        public static List<Manager> GetZakazList()
        {
            List<Manager> List = new List<Manager>();

            DataTable dt = GetData(String.Format(@"Select * FROM Online_Serv"));
            foreach (DataRow dRow in dt.Rows)
            {
                List.Add(new Manager(new ZamerModels(0), new RaschetModels((int)dRow["UserId"], (string)dRow["UserName"], (Enums.category)dRow["Type"],(int)dRow["Form"],(string)dRow["Wishes"]),(DateTime)dRow["Date"],(int)dRow["ManagerId"], (string)dRow["ManagerName"],(int)dRow["Status"],(string)dRow["Comments"], (int)dRow["Id"]));
            }

            DataTable ds = GetData(String.Format(@"Select * FROM Zamer"));
            foreach (DataRow dRow in ds.Rows)
            {
                List.Add(new Manager(new ZamerModels((int)dRow["UserId"], (string)dRow["UserName"], (string)dRow["Phone"], (string)dRow["Address"], (string)dRow["Time"]), new RaschetModels(0), (DateTime)dRow["Date"], (int)dRow["ManagerId"], (string)dRow["ManagerName"], (int)dRow["Status"], (string)dRow["Comments"], (int)dRow["Id"]));
            }
            return List;
        }

        public static void EditZakaz(Manager model, int Table)
        {
            SetData(String.Format(@"Update {0} set Status = {1}, Comments = N'{2}' WHERE Id = {3} AND ManagerId = {4}", Enums.GetZakazType(Table), model.Status, model.Comments, model.Id, model.ManagerId));
        }

        public static void TakeInWork(int Id, int Table, int ManagerId)
        {
            SetData(String.Format(@"Update {0} set Status = 2, ManagerId = {1}, ManagerName = N'{2}' WHERE Id = {3}", Enums.GetZakazType(Table), ManagerId, GetUserName(ManagerId), Id));
        }
        #endregion

        #region Content
        public static Models.ContentModel GetContent(string alias)
        {
            if (alias == "") return new ContentModel();
            DataTable dt = GetData("select top 1 * from Content where Alias = N'" + alias + "'");
            if (dt.Rows.Count == 0) return new ContentModel();
            return new ContentModel((int)dt.Rows[0]["Id"], dt.Rows[0]["Alias"].ToString(), dt.Rows[0]["Title"].ToString(), (DateTime)dt.Rows[0]["Date"], (bool)dt.Rows[0]["IsPublished"], dt.Rows[0]["Brief"].ToString(), dt.Rows[0]["Description"].ToString());
        }

        public static int SetContent(Models.ContentModel model)
        {
            DataTable result = new DataTable();

            string myConnString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            if (string.IsNullOrEmpty(myConnString)) return 0;
            //int id, string alias, string title, DateTime date, bool isPublished, string author, string brief, string descr, string tags, Enums.TypeContent typeContent, int menuWis, int menuborw
            string queryString = String.Format(@"Insert into Content ( Alias, Title, Date, IsPublished, Brief, Description) 
                                                                VALUES(N'{0}',N'{1}', @date,{2},N'{3}',N'{4}') SELECT SCOPE_IDENTITY() AS [SCOPE_IDENTITY]",
                                                                model.Alias, model.Title, model.IsPublished ? 1 : 0, model.Brief == null ? "" : model.Brief.Replace("'", "''"), model.Description.Replace("'", "''"));


            SqlConnection conn = new SqlConnection(myConnString);
            SqlCommand command = new SqlCommand(queryString, conn);

            command.Parameters.Add("@date", SqlDbType.SmallDateTime);
            command.Parameters["@date"].Value = model.Date;

            //try
            //{
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(command);
            da.Fill(result);
            conn.Close();
            //}
            //catch (Exception) {
            //    return -1;
            //}

            if (result.Rows.Count == 0) return 0;
            return int.Parse(result.Rows[0]["SCOPE_IDENTITY"].ToString());
        }

        public static void UpdateContent(Models.ContentModel model)
        {
            string myConnString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            if (string.IsNullOrEmpty(myConnString)) return;

            string queryString = String.Format(@"Update Content set Alias = N'{0}', Title = N'{1}', Date = @date, IsPublished = {2}, Brief = N'{3}', Description = N'{4}' where Id = {5}",
                                                                             model.Alias, model.Title, model.IsPublished ? 1 : 0, model.Brief == null ? "" : model.Brief.Replace("'", "''"), model.Description.Replace("'", "''"), model.id);

            SqlConnection conn = new SqlConnection(myConnString);
            SqlCommand command = new SqlCommand(queryString, conn);

            command.Parameters.Add("@date", SqlDbType.SmallDateTime);
            command.Parameters["@date"].Value = model.Date;

            conn.Open();
            command.ExecuteNonQuery();
            conn.Close();

        }

        public static ListNews getListNews(bool IsPublished, int page)
        {
            //AND TypeContent = 0
            string query = "";
                query = string.Format(@"
                            declare @myCount int;
                            SELECT     @myCount = COUNT(Id) 
                            FROM            Content
                            WHERE           IsPublished = {0}

                            select top 10 Id, Alias, Title, [Date], IsPublished, Brief, Description, @myCount As count
                            from Content 
                            where IsPublished = {0} AND (Id not in (Select top ({1}) Id from Content where IsPublished = {0} Order By [Date] desc))
                            Order By [Date] desc ", IsPublished ? 1 : 0, 10 * (page - 1));
            
            DataTable dt = GetData(query);
            ListNews result = new ListNews();
            if (dt != null && dt.Rows.Count > 0)
            {
                result.count = (int)dt.Rows[0]["count"];
                result.page = page;
                result.news = new List<ContentModel>();
                foreach (DataRow dRow in dt.Rows)
                {
                    result.news.Add(new ContentModel((int)dRow["Id"], dRow["Alias"].ToString(), dRow["Title"].ToString(), (DateTime)dRow["Date"], (bool)dRow["IsPublished"], dRow["Brief"].ToString(), dRow["Description"].ToString()));
                }
            }
            return result;
        }
        #endregion

    }
}
