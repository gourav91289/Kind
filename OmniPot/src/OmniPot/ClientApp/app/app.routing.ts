import { ModuleWithProviders } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";

import { LoginComponent } from "./components/login/login.component";

const routes: Routes = [
    {
        path: '',
        redirectTo: '',
        pathMatch: 'full'
    },
    {
        path: 'login',
        component: LoginComponent
    }
    //{
    //    path: 'lounge/:id',
    //    component: LoungeDetailComponent
    //},
    //{
    //    path: 'explore/:id',
    //    component: PlaceDetailComponent
    //}
];

export const AppRoutingProviders: any[] = [
];

export const AppRouting: ModuleWithProviders = RouterModule.forRoot(routes);  