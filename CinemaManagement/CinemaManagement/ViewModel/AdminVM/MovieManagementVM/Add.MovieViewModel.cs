using CinemaManagement.DTOs;
using CinemaManagement.Models.Services;
using CinemaManagement.Utils;
using CinemaManagement.Views;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CinemaManagement.ViewModel.AdminVM.MovieManagementVM
{

    public partial class MovieManagementViewModel : BaseViewModel
    {
        public ICommand LoadAddMovieCM { get; set; }
        public async Task SaveMovieFunc(Window p)
        {
            if (filepath != null && IsValidData())
            {
                List<GenreDTO> temp = new List<GenreDTO>();
                temp.Add(movieGenre);

                string movieImage = await CloudinaryService.Ins.UploadImage(filepath);

                if (movieImage is null)
                {
                    new MessageBoxCustom("Thông báo", "Lỗi phát sinh trong quá trình lưu ảnh. Vui lòng thử lại", MessageType.Error, MessageButtons.OK);
                    return;
                }

                MovieDTO movie = new MovieDTO
                {
                    DisplayName = movieName,
                    Country = movieCountry,
                    Director = movieDirector,
                    Description = movieDes,
                    Image = movieImage,
                    Genres = temp,
                    ReleaseYear = int.Parse(movieYear),
                    RunningTime = int.Parse(movieDuration),
                };



                (bool successAddMovie, string messageFromAddMovie, MovieDTO newMovie) = await MovieService.Ins.AddMovie(movie);

                if (successAddMovie)
                {
                    isSaving = false;
                    new MessageBoxCustom("Thông báo", messageFromAddMovie, MessageType.Success, MessageButtons.OK);

                    LoadMovieListView(Operation.CREATE, newMovie);
                    p.Close();
                }
                else
                {
                    new MessageBoxCustom("Lỗi", messageFromAddMovie, MessageType.Error, MessageButtons.OK);

                }
            }
            else
            {
                new MessageBoxCustom("Cảnh báo", "Vui lòng nhập đủ thông tin!", MessageType.Warning, MessageButtons.OK);

            }
        }
    }


}
