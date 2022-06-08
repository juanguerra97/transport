import {NgModule} from '@angular/core';
import {HomeMaterialesComponent} from './home-materiales/home-materiales.component';
import {RouterModule, Routes} from "@angular/router";
import {CoreModule} from "../../core/core.module";
import {NewMaterialComponent} from './new-material/new-material.component';
import {EditMaterialComponent} from './edit-material/edit-material.component';

const routes: Routes = [
  {path: '', pathMatch: 'full', component: HomeMaterialesComponent},
  {path: 'nuevo', component: NewMaterialComponent},
  {path: 'editar/:id', component: EditMaterialComponent},
];

@NgModule({
  declarations: [
    HomeMaterialesComponent,
    NewMaterialComponent,
    EditMaterialComponent
  ],
  imports: [
    CoreModule,
    RouterModule.forChild(routes),
  ]
})
export class MaterialesModule {
}
