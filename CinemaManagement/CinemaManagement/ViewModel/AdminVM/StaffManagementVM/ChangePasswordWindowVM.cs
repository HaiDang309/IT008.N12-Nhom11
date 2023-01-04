using CinemaManagement.Models.Services;
using CinemaManagement.Utils;
using CinemaManagement.Views;
using System.Threading.Tasks;
using System.Windows;

namespace CinemaManagement.ViewModel.AdminVM.StaffManagementVM
{
    public partial class StaffManagementViewModel : BaseViewModel
    {
        public async Task ChangePass(Window p)
        {

            (bool isValid, string error) = IsValidPassword(Utils.Operation.UPDATE_PASSWORD);

            if (isValid)
            {
                (bool updatePassSuccesss, string message) = await StaffService.Ins.UpdatePassword(SelectedItem.Id, MatKhau);
                if (updatePassSuccesss)
                {
                    p.Close();
                    new MessageBoxCustom("Thông báo", message, MessageType.Success, MessageButtons.OK);

                }
                else
                {
                    new MessageBoxCustom("Lỗi", message, MessageType.Error, MessageButtons.OK);

                }

            }
            else
            {
                new MessageBoxCustom("Cảnh báo", error, MessageType.Warning, MessageButtons.OK);

            }
        }
        public (bool valid, string error) IsValidPassword(Operation oper)
        {
            if (oper == Operation.CREATE || oper == Operation.UPDATE_PASSWORD)
            {
                if (string.IsNullOrEmpty(MatKhau))
                {
                    return (false, "Vui lòng nhập mật khẩu");
                }
                if (MatKhau != RePass)
                    return (false, "Mật khẩu và mật khẩu nhập lại không trùng khớp!");
            }
            return (true, null);
        }
    }
}
