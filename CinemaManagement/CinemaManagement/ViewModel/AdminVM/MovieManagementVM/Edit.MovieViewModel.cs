using CinemaManagement.DTOs;
using CinemaManagement.Models.Services;
using CinemaManagement.Utils;
using CinemaManagement.Views;
using CinemaManagement.Views.Admin.QuanLyPhimPage;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CinemaManagement.ViewModel.AdminVM.MovieManagementVM
{
    public partial class MovieManagementViewModel : BaseViewModel
    {

        public ICommand LoadEditMovieCM { get; set; }

        public async void LoadEditMovie(EditMovie w1)
        {
            List<GenreDTO> tempgenre = new List<GenreDTO>(SelectedItem.Genres);

            IsImageChanged = false;
            movieID = SelectedItem.Id.ToString();
            movieName = SelectedItem.DisplayName;
            movieGenre = tempgenre[0];
            movieYear = SelectedItem.ReleaseYear.ToString(); ;
            movieDirector = SelectedItem.Director;
            movieCountry = SelectedItem.Country;
            movieDuration = SelectedItem.RunningTime.ToString();
            movieDes = SelectedItem.Description;
            w1._Genre.Text = tempgenre[0].DisplayName;
            Image = SelectedItem.Image;

            ImageSource = await CloudinaryService.Ins.LoadImageFromURL(SelectedItem.Image);
        }
        public async Task UpdateMovieFunc(Window p)
        {
            if (movieID != null && IsValidData())
            {
                List<GenreDTO> temp = new List<GenreDTO>();
                temp.Add(movieGenre);

                MovieDTO movie = new MovieDTO
                {
                    Id = int.Parse(movieID),
                    DisplayName = movieName,
                    Country = movieCountry,
                    Director = movieDirector,
                    Description = movieDes,
                    Genres = temp,
                    ReleaseYear = int.Parse(movieYear),
                    RunningTime = int.Parse(movieDuration),
                };

                if (IsImageChanged)
                {
                    if (Image != null)
                    {
                        await CloudinaryService.Ins.DeleteImage(Image);
                    }

                    movie.Image = await CloudinaryService.Ins.UploadImage(filepath);




                    if (movie.Image is null)
                    {
                        new MessageBoxCustom("Thông báo", "Lưu ảnh thất bại. Vui lòng thử lại", MessageType.Error, MessageButtons.OK);
                        return;
                    }
                }
                else
                {
                    movie.Image = Image;
                }

                (bool successUpdateMovie, string messageFromUpdateMovie) = await MovieService.Ins.UpdateMovie(movie);

                if (successUpdateMovie)
                {
                    isSaving = false;
                    new MessageBoxCustom("Thông báo", messageFromUpdateMovie, MessageType.Success, MessageButtons.OK);

                    LoadMovieListView(Operation.UPDATE, movie);

                    p.Close();
                }
                else
                {
                    new MessageBoxCustom("Lỗi", messageFromUpdateMovie, MessageType.Error, MessageButtons.OK);

                }
            }
            else
            {
                new MessageBoxCustom("Cảnh báo", "Vui lòng nhập đủ thông tin!", MessageType.Warning, MessageButtons.OK);

            }
        }

    }
}
