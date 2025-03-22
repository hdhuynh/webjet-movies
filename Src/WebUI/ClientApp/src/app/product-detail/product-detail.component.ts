import { Component, OnInit, ChangeDetectionStrategy } from '@angular/core';
import { ProductDetailVm } from '../backend-api';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-product-detail',
  templateUrl: './product-detail.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ProductDetailComponent implements OnInit {
  public product: ProductDetailVm;
  public detailKeys: string[] = [];

  constructor(private bsModalService: BsModalService, private bsModalRef: BsModalRef) {}

  ngOnInit() {
    this.product = this.bsModalService.config.initialState['product'] as ProductDetailVm;

    this.detailKeys = Object
      .keys(this.product)
      .filter(keys => keys !== 'id');
  }

  public closeModal() {
    this.bsModalRef.hide();
  }

}
