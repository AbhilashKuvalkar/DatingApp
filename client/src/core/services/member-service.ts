import { HttpClient, HttpHeaders } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Member } from '../../types/member';
import { AccountService } from './account-service';
import { Photo } from '../../types/photo';

@Injectable({
  providedIn: 'root',
})
export class MemberService {
  private readonly httpClient = inject(HttpClient);
  private baseUrl: string = environment.apiUrl;

  getMembers() {
    return this.httpClient.get<Member[]>(this.baseUrl + "members");
  }

  getMember(id: string) {
    return this.httpClient.get<Member>(this.baseUrl + "members/" + id);
  }

  getMemberPhotos(id: string) {
    return this.httpClient.get<Photo[]>(this.baseUrl + "members/" + id + "/photos");
  }
}
