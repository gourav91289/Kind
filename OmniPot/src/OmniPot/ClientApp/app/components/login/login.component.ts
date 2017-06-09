import { Component, OnInit } from "@angular/core";
import { LoginViewModel } from '../../viewmodels/account/login';

@Component({
    selector: "login",
    templateUrl: './template/login/login.component.html',    
})

export class LoginComponent implements OnInit {

    model = new LoginViewModel('','');;  
    logoImagePath: string;
    isPasswordRembember: boolean;

    constructor() {
        this.logoImagePath = './images/logo.png';
        this.isPasswordRembember = true;
    }

    
    ngOnInit() {
    }

    onLoginSubmit() {
        alert(this.model.username);      
       
    }

}