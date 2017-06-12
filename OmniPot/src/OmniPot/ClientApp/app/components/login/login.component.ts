import { Component, OnInit } from "@angular/core";
import { FormBuilder, Validators } from '@angular/forms';
import { Title } from '@angular/platform-browser';
import { Router } from '@angular/router';
import { Http } from '@angular/http';
import { LoginViewModel } from '../../viewmodels/account/login.interface';
import { ValidationService } from '../../services/validation.service';
import { AuthService } from '../../security/auth.service';

@Component({
    selector: "login",
    template: `<div class="container">
    <div class="col-lg-5 col-sm-6 col-lg-offset-1 child">
        <img [src]="logoPath" style="margin:40% 0 0 0" class="img-responsive" alt="AGRSoft">
    </div>

    <div class="col-lg-4 col-sm-6 col-lg-offset-1 login-font">
        <form [formGroup]="loginForm" novalidate (submit)="login(loginForm.value)">
            <h1 class="h1">Log In</h1>
            <span class="font-size11">Please enter your username and password to login</span>

            <div class="form-group offset-top-3 ">
                <input type="text" class="form-control" formControlName="email" id="email" placeholder="Username" title="Please enter you username">
                <control-messages [control]="loginForm.controls.email" class="help-block"></control-messages>
            </div>

            <div class="form-group">
                <input type="password" class="form-control" id="password" name="password" formControlName="password" placeholder="Password" title="Please enter your password">
                <control-messages [control]="loginForm.controls.password" class="help-block"></control-messages>
            </div>
            <div id="loginErrorMsg" *ngIf="isLoginError" class="alert alert-danger">{{loginErrorMessage}}</div>
            <div class="checkbox">
                <label class="font-size11">
                    <input type="checkbox" name="remember" id="remember" formControlName="rememberMe">Remember login
                </label>
                <a href="#" class="pull-right font-size11">Forgot Password</a>
            </div>
            <button type="submit" class="btn btn-primary btn-custom offset-top-2 offset-bottom-3"
                    [disabled]="!loginForm.valid"><i class="fa fa-angle-right"></i>Log In </button>
        </form>
        <hr />
        <a href="#" class="font-size14">Need help with login?</a><br>
        <a href="#" class="font-size12"><strong>Click here</strong></a> <span class="font-size12">to read our FAQ section.</span>
    </div>
</div>`,
})

export class LoginComponent implements OnInit {
    loginForm: any;
    logoPath: string;
    public isLoginError: boolean;
    loginErrorMessage: string;
    public submitted: boolean;
    public events: any[] = [];

    constructor(
        public router: Router,
        private titleService: Title,
        public http: Http,
        private authService: AuthService,
        private formBuilder: FormBuilder) {
        this.logoPath = './images/logo.png';       
    }

    ngOnInit() {
        this.loginForm = this.formBuilder.group({
            'email': ['', [Validators.required, ValidationService.emailValidator]],
            'password': ['', [Validators.required, ValidationService.passwordValidator]],
            'rememberMe': [true]
        });

        //setting title
        this.setTitle('Agrisoft - Login');

        // subscribe to form changes  
        this.subcribeToFormChanges();
    }

    // wrapper to the Angular title service.
    setTitle(newTitle: string) {
        this.titleService.setTitle(newTitle);
    }

    subcribeToFormChanges() {
        const myFormStatusChanges$ = this.loginForm.statusChanges;
        const myFormValueChanges$ = this.loginForm.valueChanges;

        myFormStatusChanges$.subscribe(x => this.events.push({ event: 'STATUS_CHANGED', object: x }));
        myFormValueChanges$.subscribe(x => this.events.push({ event: 'VALUE_CHANGED', object: x }));
    }

    // post the user's login details to server, if authenticated token is returned, then token is saved to session storage
    login(user: LoginViewModel) {
        //alert(`Email: ${user.email}, Password: ${user.password}, Remember: ${user.rememberMe}`);
        this.isLoginError = false;
        if (this.loginForm.dirty && this.loginForm.valid) {            
            this.http.post('/api/account/authenticate', user, { headers: this.authService.contentHeaders() })
                .subscribe(response => {
                    // success, save the token to session storage
                    var result = response.json();
                    if (result.Succeeded) {
                        this.authService.login(user, result);
                        this.router.navigate(['/dashboard']);
                    }
                    else {
                        this.isLoginError = true;
                        this.loginErrorMessage = result.Message;
                    }
                },
                error => {
                    // failed; TODO: add some nice toast / error handling
                    console.log(error.text());
                });
        }
    }
}