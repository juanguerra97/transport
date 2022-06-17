

export function getErrorMessage(error: any) {
  if (error.response) {
    const response = JSON.parse(error.response);
    console.log(response);
    if (response.errors) {
      const msg = (Object.values(response.errors) as any[])[0][0];

      return msg;
    }
    if (response.detalle) {
      return response.detalle;
    }
  }
  return error.message;
}
