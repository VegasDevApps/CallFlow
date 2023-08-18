import { Component, OnInit } from '@angular/core';
import { MainServiceService } from '../services/main-service.service';
import { Client } from '../interfaces/client.interface';
import { Validators, FormBuilder, FormGroup, } from '@angular/forms';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {

  constructor(private mainService: MainServiceService, private fb: FormBuilder){}
  
  searchResult: Client[] = [];
  
  searchForm = this.fb.nonNullable.group({
    findTo: ['', Validators.required],
    findBy: ['', Validators.required]
  });

  ngOnInit(): void {
  
  }
  
  onSubmit(){
    this.mainService
      .search(this.searchForm.value.findBy!, this.searchForm.value.findTo!)
      .subscribe(res => this.searchResult = res);
  }




}
