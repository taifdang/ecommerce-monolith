import { apiAuth } from "@/shared/lib/api-auth";

export const fetchOrder = () => apiAuth.get(`/api/v1/orders`);

export const placeOrder = (_customerId, _method, _provider, _street, _city, _zipCode, ) =>
  apiAuth.post(`api/v1/orders`, {
    customerId: _customerId,
    method: _method,
    provider: _provider,
    street: _street,
    city: _city,
    zipCode: _zipCode,
   
  });
