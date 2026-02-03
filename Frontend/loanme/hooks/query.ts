import { Result, ResultErrors } from "@/types/result";
import { get } from "@/utils/http";
import { useQuery, UseQueryOptions } from "@tanstack/react-query";

const formatFullUrl = (url: string) => {
  let loanMeDomain = process.env.NEXT_PUBLIC_LOANME_API_URL;

  if (!loanMeDomain) {
    throw new Error("NEXT_LOANME_API_URL is not set.");
  }

  if (loanMeDomain[loanMeDomain.length - 1] === "/") {
    loanMeDomain = loanMeDomain.slice(0, loanMeDomain.length - 1);
  }

  return loanMeDomain + "/api" + url;
};

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
