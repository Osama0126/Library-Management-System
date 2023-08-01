using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Immutable;
using System.Data.SqlClient;
using System.Reflection;
using System.Windows.Input;

namespace Member_Details.Pages.Members
{
    public class EditModel : PageModel
    {
        public MemberInfo memberInfo = new MemberInfo();
        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {
            String id = Request.Query["id"];
            try
            {
                String connectionString = "Data Source=OSAMA126\\SQLEXPRESS;Initial Catalog=\"Member list\";Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Members WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if(reader.Read())
                            {
                                memberInfo.id = "" + reader.GetInt32(0).ToString();
                                memberInfo.name = reader.GetString(1);
                                memberInfo.email = reader.GetString(2);
                                memberInfo.phone = reader.GetString(3);
                                memberInfo.dob = reader.GetDateTime(4).ToString("dd-MM-yyyy");
                                memberInfo.gender = reader.GetString(5);
                            }
                        }
                    }
                }
                {

                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

        public void OnPost()
        {

            memberInfo.id = Request.Form["id"]; 
            memberInfo.name = Request.Form["name"];
            memberInfo.email = Request.Form["email"];
            memberInfo.phone = Request.Form["phone"];
            memberInfo.dob = Request.Form["dob"];
            memberInfo.gender= Request.Form["gender"];

            if(memberInfo.id.Length==0 || memberInfo.name.Length==0 || memberInfo.email.Length==0
                || memberInfo.phone.Length==0 || memberInfo.dob.Length==0 ||memberInfo.gender.Length==0)
            {
                errorMessage = "All fields are mandatory";
                return;
            }
            try
            {
                String connectionString = "Data Source=OSAMA126\\SQLEXPRESS;Initial Catalog=\"Member list\";Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE Members " +
                                 "SET name=@name, email=@email, phone=@phone, dob=@dob, gender=@gender " +
                                 "WHERE id=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", memberInfo.name);
                        command.Parameters.AddWithValue("@email", memberInfo.email);
                        command.Parameters.AddWithValue("@phone", memberInfo.phone);
                        command.Parameters.AddWithValue("@dob", memberInfo.dob);
                        command.Parameters.AddWithValue("@gender", memberInfo.gender);
                        command.Parameters.AddWithValue("@id", memberInfo.id);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/Members/Index");
        }
    }
}
