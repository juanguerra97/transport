import {Injectable} from '@angular/core';
import {AuthorizeService, Roles} from "../../api-authorization/authorize.service";
import {BehaviorSubject} from "rxjs";
import {INavData} from "@coreui/angular";

@Injectable({
  providedIn: 'root'
})
export class MenuService {

  private _menuSubject = new BehaviorSubject<INavData[]>([]);
  public menuData = this._menuSubject.asObservable();

  constructor(
    private authorizeService: AuthorizeService,
  ) {
    this.authorizeService.getUser().subscribe({
      next: user => {
        if (!user) {
          this._menuSubject.next([]);
        } else {
          const menu: INavData[] = [];

          if (user.role?.includes(Roles.ADMINISTRATOR)) {
            menu.push({
                title: true,
                name: 'Administración'
              },
              {
                name: 'Catálogos',
                icon: 'fa fa-solid fa-table',
                children: [
                  {name: 'Plantas', url: '/admin/catalogo/plantas'},
                  {name: 'Bodegas', url: '/admin/catalogo/bodegas'},
                  {name: 'Materiales', url: '/admin/catalogo/materiales'},
                  {name: 'Medidas', url: '/admin/catalogo/medidas'},
                  {name: 'Proveedores', url: '/admin/catalogo/proveedores'}
                ]
              });
          }
          if (user.role?.includes(Roles.ADMIN_BODEGA)) {
            menu.push({
                title: true,
                name: 'Inventario'
              },
              {
                name: 'Bodegas',
                icon: 'fa fa-solid fa-warehouse',
                url: '/inventario'
              });
          }
          this._menuSubject.next(menu);
        }
      }
    });
  }
}
