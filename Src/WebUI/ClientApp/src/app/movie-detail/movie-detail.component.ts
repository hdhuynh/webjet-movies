import { Component, OnInit, ChangeDetectionStrategy } from '@angular/core';
import { MovieDetailVm } from '../backend-api';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-movie-detail',
  templateUrl: './movie-detail.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class MovieDetailComponent implements OnInit {
  public movie: MovieDetailVm;
  public detailKeys: string[] = [];

  constructor(private bsModalService: BsModalService, private bsModalRef: BsModalRef) {}

  ngOnInit() {
    this.movie = this.bsModalService.config.initialState['movie'] as MovieDetailVm;

    this.detailKeys = Object
      .keys(this.movie)
      .filter(keys => keys !== 'id' && keys != 'poster' && keys != 'bestPriceProvider');
  }

  public closeModal() {
    this.bsModalRef.hide();
  }

}
