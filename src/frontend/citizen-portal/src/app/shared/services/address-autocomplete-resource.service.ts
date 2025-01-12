import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';
import { map, tap, catchError } from 'rxjs/operators';

import { ApiResource } from '@core/resources/api-resource.service';
import { ApiHttpResponse } from '@core/models/api-http-response.model';
import { ToastService } from '@core/services/toast.service';
import { AddressAutocompleteFindResponse, AddressAutocompleteRetrieveResponse } from '@shared/models/address-autocomplete.model';
import { LoggerService } from '@core/services/logger.service';

@Injectable({
  providedIn: 'root'
})
export class AddressAutocompleteResource {
  constructor(
    private apiResource: ApiResource,
    private toastService: ToastService,
    private logger: LoggerService
  ) { }

  public findAddress(searchTerm: string): Observable<AddressAutocompleteFindResponse[]> {
    return this.apiResource.get<AddressAutocompleteFindResponse[]>(`AddressAutocomplete/Find?searchTerm=${searchTerm}`)
      .pipe(
        map((response: ApiHttpResponse<AddressAutocompleteFindResponse[]>) => response.result),
        tap((response: AddressAutocompleteFindResponse[]) => this.logger.info('AUTOCOMPLETE_FIND', response)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Autocomplete could not be retrieved');
          this.logger.error('[Shared] AddressAutocompleteResource::find error has occurred: ', error);

          throw error;
        })
      );
  }

  public retrieveAddress(id: string): Observable<AddressAutocompleteRetrieveResponse[]> {
    return this.apiResource.get<AddressAutocompleteRetrieveResponse[]>(`AddressAutocomplete/Retrieve?id=${id}`)
      .pipe(
        map((response: ApiHttpResponse<AddressAutocompleteRetrieveResponse[]>) => response.result),
        tap((response: AddressAutocompleteRetrieveResponse[]) => this.logger.info('AUTOCOMPLETE_RETRIEVE', response)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Autocomplete could not be retrieved');
          this.logger.error('[Shared] AddressAutocompleteResource::find error has occurred: ', error);

          throw error;
        })
      );
  }
}
