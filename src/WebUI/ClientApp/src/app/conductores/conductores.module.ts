import {NgModule} from '@angular/core';
import {RouterModule, Routes} from "@angular/router";
import {CoreModule} from "../core/core.module";
import {HomeConductoresComponent} from './home-conductores/home-conductores.component';

const routes: Routes = [
  {path: 'entregas', component: HomeConductoresComponent,}
];

@NgModule({
  declarations: [
    HomeConductoresComponent
  ],
  imports: [
    CoreModule,
    RouterModule.forChild(routes)
  ]
})
export class ConductoresModule {
}
