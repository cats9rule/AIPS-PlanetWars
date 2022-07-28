import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable } from 'rxjs';
import { ServerErrorHandlerService } from 'src/app/core/utils/services/server-error-handler.service';
import { environment } from 'src/environments/environment.prod';
import { CreateGameDto } from '../dtos/createGameDto';
import { JoinGameDto } from '../dtos/joinGameDto';
import { SessionDto } from '../dtos/sessionDto';

@Injectable({
  providedIn: 'root',
})
export class InitGameService {
  constructor(
    private http: HttpClient,
    private errorHandler: ServerErrorHandlerService
  ) {}

  private url = environment.serverUrl + '/Session/';

  createGame(createGameDto: CreateGameDto): Observable<SessionDto> {
    console.log(createGameDto);
    return this.http
      .post<SessionDto>(this.url + 'CreateGame', createGameDto)
      .pipe(catchError(this.errorHandler.handleError));
  }

  joinGame(joinGameDto: JoinGameDto): Observable<SessionDto> {
    return this.http
      .put<SessionDto>(this.url + 'JoinGame', joinGameDto)
      .pipe(catchError(this.errorHandler.handleError));
  }
}
