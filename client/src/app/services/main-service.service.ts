import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Client } from '../interfaces/client.interface';
import { ClientDataInput } from '../interfaces/clientDataInput.interface';
import { tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class MainServiceService {

  constructor(private http: HttpClient) { }

  serverUrl = environment.serverUrl + 'api/main/';

  searchCache: Client[] = [];

  addNewClient(client: ClientDataInput){
    return this.http.post<ClientDataInput>(this.serverUrl, client);
  }

  updateClient(client: Client){
    return this.http.put<Client>(this.serverUrl, client);
  }

  search(findBy: string, findTo: string){
    const httpParams = new HttpParams()
      .set('findBy', findBy)
      .set('findTo', findTo);

    return this.http.get<Client[]>(this.serverUrl, {params: httpParams}).pipe(tap(data => this.searchCache = data));
  }
}
