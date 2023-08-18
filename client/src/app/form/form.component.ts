import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { MainServiceService } from '../services/main-service.service';
import { Client } from '../interfaces/client.interface';
import { ClientDataInput } from '../interfaces/clientDataInput.interface';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.css']
})
export class FormComponent implements OnInit {

  constructor(
    private fb: FormBuilder,
    private mainService: MainServiceService,
    private route: ActivatedRoute) { }


  updateClient?: Client;

  form = this.fb.nonNullable.group({
    CitizenID: ['', Validators.compose([Validators.required, Validators.minLength(9), Validators.maxLength(9), Validators.pattern('^[0-9]*$')])],
    PhoneNumber: ['', Validators.compose([Validators.required, Validators.minLength(6), Validators.maxLength(50), Validators.pattern('^[0-9]*$')])],
    FirstName: ['', Validators.maxLength(100)],
    LastName: ['', Validators.maxLength(100)],
    BitrthDate: [''],
    Notes: ['', Validators.maxLength(10)],
  });

  ngOnInit(): void {
    const idToEdit = this.route.snapshot.paramMap.get('id');
    if (idToEdit) {
      this.updateClient = this.mainService.searchCache.find(c => c.Id.toString() === idToEdit);
      if (this.updateClient) {
        this.populateForm();
      }
    }
  }

  submit() {

    const formData = this.form.value;
    const client: ClientDataInput = {
      CitizenID: formData.CitizenID!,
      PhoneNumber: formData.PhoneNumber!,
      BitrthDate: formData.BitrthDate,
      FirstName: formData.FirstName,
      LastName: formData.LastName,
      Notes: formData.Notes
    };

    if(this.updateClient)
    {
      this.updateExistedClient({...this.updateClient, ...client});
    } else {
      this.addNewClient(client);
    }

  }

  addNewClient(client: ClientDataInput) {
    this.mainService.addNewClient(client).subscribe({
      next: res => {
        console.log(res);
      },
      error: err => {
        console.log(err);
      }
    });
  }

  updateExistedClient(client: Client) {
    this.mainService.updateClient(client).subscribe({
      next: res => {
        console.log(res);
      },
      error: err => {
        console.log(err);
      }
    });
  }


  populateForm() {

    let birthdate;
    if(this.updateClient?.BitrthDate)
    {
      birthdate = this.updateClient?.BitrthDate.toString().substring(0, 10);
    }

    this.form.patchValue({
      CitizenID: this.updateClient?.CitizenID,
      PhoneNumber: this.updateClient?.PhoneNumber,
      FirstName: this.updateClient?.FirstName,
      LastName: this.updateClient?.LastName,
      BitrthDate: birthdate,
      Notes: this.updateClient?.Notes
    });

    console.log('Vegas ', this.updateClient?.BitrthDate.toString().substring(0, 10));
  }
}
