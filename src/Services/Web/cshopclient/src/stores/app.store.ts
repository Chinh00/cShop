import { atom } from "jotai";

export type AppStore = {
    isAuthenticated: boolean;
}

export const AppState = atom<AppStore>({
    isAuthenticated: false,
});

