import { ApplicationNumberContext } from "@/utils/contexts";
import { useContext } from "react";

export const useApplicationNumberContext = () => {
  const context = useContext(ApplicationNumberContext);
  return context;
};
