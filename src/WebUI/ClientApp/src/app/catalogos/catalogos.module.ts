import {NgModule} from '@angular/core';
import {RouterModule, Routes} from "@angular/router";
import {CoreModule} from "../core/core.module";

const routes: Routes = [
  {path: 'plantas', loadChildren: () => import('./plantas/plantas.module').then(m => m.PlantasModule)}
];

@NgModule({
  declarations: [],
  imports: [
    CoreModule,
    RouterModule.forChild(routes)
  ]
})
export class CatalogosModule {
}
