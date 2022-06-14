import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
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
  ]
})
export class CoreModule {
}
