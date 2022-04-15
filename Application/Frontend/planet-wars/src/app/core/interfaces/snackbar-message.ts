export interface SnackbarMessage {
  type: 'Info' | 'Success' | 'Error' | 'Warning';
  contents: string;
}
