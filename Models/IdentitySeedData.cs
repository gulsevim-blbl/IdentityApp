using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IdentityApp.Models
{
    public class IdentitySeedData
    {
        //ilk başta hangi kullanıcıların eklenmesi gerektiğini belirten kodlar buraya gelecek
        private const string adminUser = "admin";
        private const string adminPassword = "admin_123";

        public static async void IdentityTestUser(IApplicationBuilder app)
        {
            var context = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<IdentityContext>();

            if(context.Database.GetAppliedMigrations().Any())
            {
                context.Database.Migrate();
            }

            var userManager = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<UserManager<AppUser>>();

            var user = await userManager.FindByNameAsync(adminUser);

            if(user == null)
            {
                user = new AppUser {
                    FullName ="Gül Sevim Bülbül",
                    UserName = adminUser,
                    Email = "admin@gulsevimblbl.com",
                    PhoneNumber = "05457777777"                    
                };
                //bunler haricinde ad soyad şehir cinsiyet gibi  bilgileri ıdentityuser da tutamıyoruz.IdentityUserdan ürettiğimiz ekstra bir sınıf var orada tutulur.

                await userManager.CreateAsync(user, adminPassword); //şifreyi hashleyip veritabanına kaydedeceğimiz için IdentityUser ın içine eklemedik doğrudan.
            }
        }
    } 
}