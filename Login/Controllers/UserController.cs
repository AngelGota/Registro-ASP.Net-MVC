using Login.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace Login.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            using (SqlConnection con = new(Configuration["ConnectionStrings:cadCon"]))
            {
                using (SqlCommand cmd = new("sp_read_users", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    con.Open();
                    SqlDataAdapter da = new(cmd);
                    DataTable dt = new();
                    da.Fill(dt);
                    da.Dispose();
                    List<UserModel> list = new();
                    
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        list.Add(new UserModel()
                        {
                            IdUser = Convert.ToInt32(dt.Rows[i][0]),
                            Name = (dt.Rows[i][1]).ToString(),
                            Age = Convert.ToInt32(dt.Rows[i][2]),
                            Mail = (dt.Rows[i][3]).ToString(),
                            Password = (dt.Rows[i][4]).ToString()
                        });
                    }
                    ViewBag.User = list;
                    con.Close();
                }
                return View();
            }
        }

        public IConfiguration Configuration { get; }
        public UserController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IActionResult Register() 
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(UserModel usuario) 
        {
            using (SqlConnection con = new(Configuration["ConnectionStrings:cadCon"]))
            {
                using (SqlCommand cmd = new("sp_register_users", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@namUser", System.Data.SqlDbType.VarChar).Value=usuario.Name;
                    cmd.Parameters.Add("@ageUser", System.Data.SqlDbType.Int).Value = usuario.Age;
                    cmd.Parameters.Add("@mailUser", System.Data.SqlDbType.VarChar).Value = usuario.Mail;
                    cmd.Parameters.Add("@passUser", System.Data.SqlDbType.VarChar).Value = usuario.Password;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return Redirect("Index");
        }
    }
}
