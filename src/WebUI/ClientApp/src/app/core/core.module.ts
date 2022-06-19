import {NgModule} from '@angular/core';
import {CommonModule, DatePipe} from '@angular/common';
import {PrimeNgModule} from "./prime-ng.module";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {CoreUiModule} from "./core-ui.module";

@NgModule({
  declarations: [],
  imports: [],
  exports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    PrimeNgModule,
    CoreUiModule,
  ],
  providers: [
    DatePipe,
  ]
})
export class CoreModule {
}
