import { Component ,OnInit} from "@angular/core";
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';
import { User } from "../../../viewmodels/admin/users/userdetails.interface";

//declare var module: {  id: string;}

@Component({
    //moduleId: module.id,
    selector: "userreg",
    templateUrl: './template/admin/users/registration.component.html',
    //styleUrls: ['./css/bootstrap','./css/custom-stylesheet']
})

export class RegistrationComponent implements OnInit {    
   public myForm: FormGroup;
    public submitted: boolean;
    public events: any[] = [];

    constructor(private _fb: FormBuilder) { }

    ngOnInit() {

    this.myForm = this._fb.group({
            name: ['', [<any>Validators.required, <any>Validators.minLength(5)]],
            address: this._fb.group({
                street: ['', <any>Validators.required],
                postcode: ['8000']
            })
        });
  
        // subscribe to form changes 
         this.subcribeToFormChanges();
        
        // Update single value
        (<FormControl>this.myForm.controls['name'])
            .setValue('John', { onlySelf: true });
        
        // Update form model
        // const people = {
        // 	name: 'Jane',
        // 	address: {
        // 		street: 'High street',
        // 		postcode: '94043'
        // 	}
        // };
        
        // (<FormGroup>this.myForm)
        //     .setValue(people, { onlySelf: true });


 }
    

    subcribeToFormChanges() {
        const myFormStatusChanges$ = this.myForm.statusChanges;
        const myFormValueChanges$ = this.myForm.valueChanges;
        
        myFormStatusChanges$.subscribe(x => this.events.push({ event: 'STATUS_CHANGED', object: x }));
        myFormValueChanges$.subscribe(x => this.events.push({ event: 'VALUE_CHANGED', object: x }));
    }

    save(model: User, isValid: boolean) {
        this.submitted = true;
        console.log(model, isValid);
    }
}