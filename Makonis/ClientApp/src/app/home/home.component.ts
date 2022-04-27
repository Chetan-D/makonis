import { Component, ViewChild, ElementRef } from '@angular/core';
import { FormGroup, FormControl, Validators} from '@angular/forms';
import { Person } from '../../model/person';
import { BackendService } from '../../services/backend';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
    formdata;
    firstname;
    lastname;
    responseData;
    
    @ViewChild('alert', { static: true }) alert: ElementRef;

    
    constructor(private backendService: BackendService) {        
    }
    ngOnInit() {
       
        this.formdata = new FormGroup({
            firstname: new FormControl("", Validators.compose([
                Validators.required
            ])),
            lastname: new FormControl("", Validators.compose([
                Validators.required
            ]))
        });
    }
    onClickSubmit(data: Person) {
              
        this.backendService.insertPersonDetails(data).subscribe(
            (response) => {  
                this.responseData = response;
                this.alert.nativeElement.classList.add('show');
                this.formdata.reset();
            }
        );       
    }

    closeAlert() {
        this.alert.nativeElement.classList.remove('show');
    }
}

