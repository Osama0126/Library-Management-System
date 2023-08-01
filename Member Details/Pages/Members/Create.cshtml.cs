using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Member_Details.Pages.Members
{
    public class CreateModel : PageModel
    {

        public MemberInfo memberInfo = new MemberInfo();
        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {
        }

        public void OnPost()
        {
            memberInfo.name = Request.Form["name"];
            memberInfo.email = Request.Form["email"];
            memberInfo.phone = Request.Form["phone"];
            memberInfo.dob = Request.Form["dob"];
            memberInfo.gender = Request.Form["gender"];

            if (memberInfo.name.Length==0 || memberInfo.email.Length==0 ||
                memberInfo.phone.Length==0 || memberInfo.dob.Length==0 || memberInfo.gender.Length==0)
            {
                errorMessage = "All the fields required";
                return;
            }


            try
            {
                String connectionString = "Data Source=OSAMA126\\SQLEXPRESS;Initial Catalog=\"Member list\";Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO Members " +
                                 "(name,email,phone,dob,gender) VALUES" +
                                 "(@name,@email,@phone,@dob,@gender);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", memberInfo.name);
                        command.Parameters.AddWithValue("@email", memberInfo.email);
                        command.Parameters.AddWithValue("@phone", memberInfo.phone);
                        command.Parameters.AddWithValue("@dob", memberInfo.dob);
                        command.Parameters.AddWithValue("@gender", memberInfo.gender);

                        command.ExecuteNonQuery();
                    }
                }
            }

            catch(Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            memberInfo.name = "";
            memberInfo.email = "";
            memberInfo.phone = "";
            memberInfo.dob = "";
            memberInfo.gender = "";
            successMessage = "New Member Added..";

            Response.Redirect("/Members/Index");
        }
    }
}
