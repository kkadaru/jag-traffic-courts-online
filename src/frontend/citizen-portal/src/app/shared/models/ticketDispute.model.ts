import { Additional } from './additional.model';
import { Disputant } from './disputant.model';
import { Offence } from './offence.model';
export interface TicketDispute {
  violationTicketNumber: string;
  violationTime: string;
  violationDate: string;

  discountDueDate?: string;
  discountAmount: number;

  disputant: Disputant;

  // Offences
  offences: Offence[];

  // Court information
  additional: Additional;

  // derived later on
  _within30days?: boolean;
  _outstandingBalanceDue?: number;
  _totalBalanceDue?: number;
  _requestSubmitted?: boolean;
}