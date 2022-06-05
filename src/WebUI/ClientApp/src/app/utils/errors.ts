

export function getErrorMessage(error: any) {
  if (error.response) {
    const response = JSON.parse(error.response);
    console.log(response);
    if (response.errors) {
      console.log(Object.values(response.errors));
      const msg = (Object.values(response.errors) as any[])[0][0];

      return msg;
    }
  }
  return error.message;
}
