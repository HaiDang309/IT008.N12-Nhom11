using CinemaManagement.DTOs;
using CinemaManagement.Models.Services;
using CinemaManagement.Views;
using System.Threading.Tasks;
using System.Windows;

namespace CinemaManagement.ViewModel.AdminVM.StaffManagementVM
{
    public partial class StaffManagementViewModel : BaseViewModel
    {
        public async Task EditStaff(Window p)
        {
            MatKhau = SelectedItem.Password;

            if (Mail != null)
            {
                if (Mail.Trim() == "")
                {
                    Mail = null;
                }
                else
                {
                    if (!Utils.RegexUtilities.IsValidEmail(Mail))
                    {
                        new MessageBoxCustom("Cảnh báo", "Email không hợp lệ", MessageType.Warning, MessageButtons.OK);

                        return;
                    }
                }
            }

            (bool isValid, string error) = IsValidData(Utils.Operation.UPDATE);
            if (isValid)
            {
                StaffDTO staff = new StaffDTO();
                staff.Id = SelectedItem.Id;
                staff.Name = Fullname;
                staff.Gender = Gender.Content.ToString();
                staff.BirthDate = Born;
                staff.PhoneNumber = Phone;
                staff.Role = Role.Content.ToString();
                staff.StartingDate = StartDate;
                staff.Username = TaiKhoan;
                staff.Email = Mail;
                (bool successUpdateStaff, string messageFromUpdateStaff) = await StaffService.Ins.UpdateStaff(staff);

                if (successUpdateStaff)
                {
                    LoadStaffListView(Utils.Operation.UPDATE, staff);
                    p.Close();
                    new MessageBoxCustom("Thông báo", messageFromUpdateStaff, MessageType.Success, MessageButtons.OK);

                    MaskName.Visibility = Visibility.Collapsed;
                }
                else
                {
                    new MessageBoxCustom("Lỗi", messageFromUpdateStaff, MessageType.Error, MessageButtons.OK);

                }
            }
            else
            {
                new MessageBoxCustom("Cảnh báo", error, MessageType.Warning, MessageButtons.OK);

            }
        }
    }
}
