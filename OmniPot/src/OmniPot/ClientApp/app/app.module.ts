//angula Modules import
import { NgModule, enableProdMode } from "@angular/core";
import { BrowserModule, Title } from '@angular/platform-browser';
import { AppRouting } from './app.routing';
import { APP_BASE_HREF, Location } from '@angular/common';
import { FormsModule } from "@angular/forms";
import { ReactiveFormsModule } from '@angular/forms';
import { HttpModule } from "@angular/http";
import { RouterModule } from "@angular/router";
import "rxjs/Rx";

//Componets Import
import { AppComponent } from "./app.component";
import { ControlMessagesComponent } from './components/control-messages.component';
import { LoginComponent } from "./components/login/login.component";
import { RegistrationComponent } from "./components/login/registration.component";
//import { RegistrationComponent } from "./components/admin/users/registration.component";
import { DashboardComponent } from "./components/dashboard/dashboard.component";

//Service Import
import { ValidationService } from './services/validation.service';
import { AuthService } from './security/auth.service';
import { AuthGuard } from './security/auth-guard.service';


@NgModule({
    // modules
    imports: [BrowserModule, ReactiveFormsModule,FormsModule,  HttpModule, AppRouting ],
    // directives, components, and pipes
    declarations: [ControlMessagesComponent, AppComponent, LoginComponent, DashboardComponent ],    
    // providers
    providers: [ ValidationService, AuthService, AuthGuard, Title, { provide: APP_BASE_HREF, useValue: '/' } ],
    bootstrap: [ AppComponent ]
})

export class AppModule { }  