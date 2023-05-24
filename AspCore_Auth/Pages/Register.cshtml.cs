using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspCore_Auth.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspCore_Auth.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<IdentityUser> userManager;
        //ung cấp các phương thức để tạo, xóa, cập nhật và xác thực người dùng.
        //Nó có thể được sử dụng để tạo người dùng mới, xác minh thông tin đăng nhập và quản lý thông tin người dùng.

        private readonly SignInManager<IdentityUser> signManager;
        //cung cấp các phương thức để đăng nhập, đăng xuất và xác minh thông tin đăng nhập của người dùng.
        //Nó cho phép xác minh mật khẩu, tạo token đăng nhập và thực hiện các hoạt động liên quan đến đăng nhập và đăng xuất người dùng.
        [BindProperty]
        public Register Model { get; set; }

        public RegisterModel( UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signManager)
        {
            this.userManager = userManager;
            this.signManager = signManager;
            //p. Các đối tượng này được chuyển vào trong phương thức khởi tạo thông qua dependency injection,
            //giúp lớp RegisterModel có thể sử dụng các tính năng của chúng trong quá trình xử lý yêu cầu đăng ký người dùng.
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser()
                {
                    UserName = Model.Email, 
                    Email = Model.Email
                };
                var result = await userManager.CreateAsync(user,Model.Password);
                //Kiểm tra xem quá trình tạo người dùng có thành công hay không.
                //Nếu result.Succeeded trả về true, tức là người dùng đã được tạo thành công trong cơ sở dữ liệu.
                if (result.Succeeded)
                {
                    //Phương thức SignInAsync được gọi và truyền vào đối tượng user
                    //Và tham số boolean false để chỉ định rằng không cần nhớ thông tin đăng nhập.
                    await signManager.SignInAsync(user, false);
                    return RedirectToPage("Register");
                }
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                    //Nếu quá trình tạo người dùng không thành công, vòng lặp foreach sẽ lặp qua danh sách các lỗi được trả về từ result.Errors
                    //Mỗi lỗi sẽ được thêm vào ModelState thông qua ModelState.AddModelError để hiển thị trên giao diện người dùng.
                }
            }

            return Page();
        }
    }
}
