import { createContext } from "react";

interface ApplicationNumberContextType {
  applicationNumber: string;
  setApplicationNumber: (applicationNumber: string) => void;
}

export const ApplicationNumberContext = createContext<
  ApplicationNumberContextType | undefined
>(undefined);
