// import { Component, inject, OnInit } from '@angular/core';
// import { Client, ProductsListVm } from '../backend-api';
// import { saveAs } from 'file-saver';
// import { ProductDetailComponent } from '../product-detail/product-detail.component';
// import {BsModalRef, BsModalService} from "ngx-bootstrap/modal";
//
// @Component({
//   templateUrl: './products.component.html'
// })
// export class ProductsComponent implements OnInit {
//   private client = inject(Client);
//   private modalService =inject(BsModalService);
//   protected productsListVm: ProductsListVm;
//
//   ngOnInit(): void {
//     this.client.getProductsList().subscribe(result => {
//       this.productsListVm = result;
//     });
//   }
//
//   public productDetail(id: number) {
//     this.client.getProductDetail(id).subscribe(result => {
//       const initialState = {
//         product: result
//       };
//       this.modalService.show(ProductDetailComponent, {initialState});
//     });
//   }
// }
