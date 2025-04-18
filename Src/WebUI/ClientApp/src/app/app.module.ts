import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app.routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { NavTopMenuComponent } from './nav-top-menu/nav-top-menu.component';
import { NavSideMenuComponent } from './nav-side-menu/nav-side-menu.component';
import { API_BASE_URL, Client } from './backend-api';
import { CamelCaseToText } from '../pipes/camel-case-to-text';
import { ModalModule } from 'ngx-bootstrap/modal';
import { AppIconsModule } from './app.icons.module';
import { environment } from "../environments/environment";
import { ToastrModule } from 'ngx-toastr';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MovieDetailComponent } from "./movie-detail/movie-detail.component";

@NgModule({
  declarations: [
    AppComponent,
    NavTopMenuComponent,
    NavSideMenuComponent,
    HomeComponent,
    MovieDetailComponent,
    CamelCaseToText
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    FormsModule,
    AppIconsModule,
    AppRoutingModule,
    ModalModule.forRoot(),
    ToastrModule.forRoot(),
  ],
  providers: [
      { provide: API_BASE_URL, useValue: environment.apiBaseUrl },
      Client,
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
