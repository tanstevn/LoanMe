import { Result, ResultErrors } from "@/types/result";
import { formatFullUrl, get, post, put } from "@/utils/http";
import {
  useMutation,
  UseMutationOptions,
  useQuery,
  UseQueryOptions,
} from "@tanstack/react-query";

export const useApiQuery = <T>(
  url: string,
  queryParams?: any,
  options?: UseQueryOptions<T, ResultErrors>,
) => {
  const formattedUrl = formatFullUrl(url);

  return useQuery<T, ResultErrors>({
    queryKey: options?.queryKey ?? [url],
    queryFn: async () => {
      const result = await get<Result<T>>(formattedUrl, queryParams);
      return result.successful ? result.data : Promise.reject(result.errors);
    },
  });
};

export const useApiMutation = <TBody, TResponse>(
  url: string,
  action: "POST" | "PUT",
  options?: Omit<
    UseMutationOptions<TResponse, ResultErrors, TBody>,
    "queryKey"
  >,
) => {
  const formattedUrl = formatFullUrl(url);
  const act = action === "POST" ? post : put;

  return useMutation<TResponse, ResultErrors, TBody>({
    mutationFn: async (body) => {
      const result = await act<Result<TResponse>>(formattedUrl, body);
      return result.successful ? result.data : Promise.reject(result.errors);
    },
    ...options,
  });
};
