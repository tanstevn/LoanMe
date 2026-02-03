import { ResultErrors } from "@/types/result";

const serializeQueryParamsFromObject = function (
  paramsObject: any,
  path?: string,
): string[] {
  var strings = new Array<string>();

  for (var objectProperty of Object.keys(paramsObject)) {
    const value = paramsObject[objectProperty];
    let propPath = objectProperty;

    if (path) {
      if (propPath) {
        propPath = `${path}.${propPath}`;
      } else {
        propPath = `${path}`;
      }
    }

    if (value == null) {
      continue;
    }

    if (value instanceof Date) {
      strings.push(`${encodeURIComponent(propPath)}=${value.toISOString()}`);
    } else if (Array.isArray(value)) {
      if (value.length !== 0) {
        const key = encodeURIComponent(propPath);
        const arrayParams = value.map((x) => `${key}=${encodeURIComponent(x)}`);
        const joinedParams = arrayParams.join("&");

        strings.push(joinedParams);
      }
    } else if (typeof value === "object") {
      strings.push(...serializeQueryParamsFromObject(value, propPath));
    } else {
      strings.push(
        `${encodeURIComponent(propPath)}=${encodeURIComponent(value)}`,
      );
    }
  }

  return strings;
};

const common = async (
  method: any,
  url: string,
  body?: any,
  queryParams?: any,
) => {
  try {
    const request = {
      method,
      headers: {
        Accept: "application/json",
      },
    } as RequestInit;

    if (body) {
      // @ts-ignore
      request.headers["Content-Type"] = "application/json";
      request.body = JSON.stringify(body);
    }

    if (queryParams) {
      url = `${url}?${(
        serializeQueryParamsFromObject(queryParams).join("&") ?? ""
      ).trim()}`;
    }

    const response = await fetch(url, request);

    if (!response.ok) {
      return Promise.reject([
        "Something went wrong. Result: " + response.status,
      ] as ResultErrors);
    }

    return await response.json();
  } catch (error: any) {
    return Promise.resolve(
      error.response ?? {
        successful: false,
        data: null,
        errors: "Something went wrong.",
      },
    );
  }
};

export const get = async <T>(url: string, queryParams?: any): Promise<T> => {
  return common("GET", url, undefined, queryParams);
};
