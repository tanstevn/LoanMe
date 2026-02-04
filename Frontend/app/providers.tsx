"use client";

import { ApplicationNumberContext } from "@/utils/contexts";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { PropsWithChildren, useState } from "react";

const makeQueryClient = () => {
  return new QueryClient({
    defaultOptions: {
      queries: {
        staleTime: 60 * 1000,
      },
    },
  });
};

const Providers = ({ children }: PropsWithChildren) => {
  const [applicationNumber, setApplicationNumber] = useState<any>(null);

  const queryClient = makeQueryClient();

  return (
    <QueryClientProvider client={queryClient}>
      <ApplicationNumberContext.Provider
        value={{ applicationNumber, setApplicationNumber }}
      >
        {children}
      </ApplicationNumberContext.Provider>
    </QueryClientProvider>
  );
};

export default Providers;
