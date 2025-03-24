import {Component, inject, OnInit} from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import {Client, MoviesListVm} from "../backend-api";
import {BsModalService} from "ngx-bootstrap/modal";
import {MovieDetailComponent} from "../movie-detail/movie-detail.component";
declare var bootstrap: any;

@Component({
  templateUrl: "./home.component.html",
  styleUrls: ["./home.component.css"]
})
export class HomeComponent implements OnInit
{
  protected movies : any;
  private client = inject(Client);
  private modalService =inject(BsModalService);
  protected moviesListVm: MoviesListVm;
  constructor(private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.client.getMoviesList().subscribe(result => {
      this.moviesListVm = result;
    });

    //refresh the list every 5 seconds for getting the latest prices and movies
    setInterval(() => {
      this.refreshMovieList();
    }, 5000);
  }

  private refreshMovieList = () => {
    this.client.getMoviesList().subscribe(result => {
      const currentMovieIds  = this.moviesListVm.movies.map(movie => movie.id);
      for (let movie of result.movies) {

         if (currentMovieIds.indexOf(movie.id)>=0)
         {
           this.moviesListVm.movies.find(m => m.id == movie.id).price = movie.price;
         }
         else this.moviesListVm.movies.push(movie);
      }
    });
  }

  public movieDetail(movieId: string) {
    this.client.getMovieDetail(movieId).subscribe(result => {
      const initialState = {
        movie: result
      };
      this.modalService.show(MovieDetailComponent, {initialState});
    });
  }

  scrollLeft() {
    document.getElementById('movieScroll').scrollBy({ left: -200, behavior: 'smooth' });
  }
  scrollRight() {
    document.getElementById('movieScroll').scrollBy({ left: 200, behavior: 'smooth' });
  }

  showLoggedOutToast() {
    const toastRef = document.getElementById("logoutToast");
    const toast = bootstrap.Toast.getOrCreateInstance(toastRef);
    toast.show();
  }

}
