﻿//angula Modules import
import { NgModule, enableProdMode } from "@angular/core";
import { BrowserModule, Title } from '@angular/platform-browser';
import { AppRouting } from './app.routing';
import { APP_BASE_HREF, Location } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule } from "@angular/http";
import { RouterModule } from "@angular/router";
//import { DropDownsModule } from '@progress/kendo-angular-dropdowns';
import "rxjs/Rx";

//Componets Import
import { AppComponent } from "./app.component";
import { ControlMessagesComponent } from './components/control-messages.component';
import { TokenVerifyComponent } from "./components/token.verify.component";
import { LoginComponent } from "./components/login/login.component";
import { RegistrationComponent } from "./components/login/registration.component";
//import { RegistrationComponent } from "./components/admin/users/registration.component";
import { DashboardComponent } from "./components/dashboard/dashboard.component";
import { CountryListComponent } from './components/common/countryList.component';

//Service Import
import { ValidationService } from './services/validation.service';
import { AuthService } from './security/auth.service';
import { AuthGuard } from './security/auth-guard.service';


@NgModule({
    // modules
    imports: [BrowserModule, FormsModule, ReactiveFormsModule, HttpModule, AppRouting],
    // directives, components, and pipes
    declarations: [ControlMessagesComponent, AppComponent, TokenVerifyComponent, LoginComponent, DashboardComponent, RegistrationComponent, CountryListComponent ],
    // providers
    providers: [ ValidationService, AuthService, AuthGuard, Title, { provide: APP_BASE_HREF, useValue: '/' } ],
    bootstrap: [ AppComponent ]
})

export class AppModule { }  