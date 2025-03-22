import {Component, inject, OnInit} from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import {Client, MoviesListVm, ProductsListVm} from "../backend-api";
import {BsModalService} from "ngx-bootstrap/modal";

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
    this.route.queryParams.subscribe(params => {
      if (params["loggedOut"] === "true") {
        this.showLoggedOutToast();
      }
    });
    
    this.client.getMoviesList().subscribe(result => {
      this.moviesListVm = result;
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
