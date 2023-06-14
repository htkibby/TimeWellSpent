import { GoogleAuth } from "./GoogleAuth";
import { emailAuth } from "./EmailAuth";

// Checks for which log out we should do... maybe don't need this.
// other methods may work for both.

export const logout = {
  logout: function(navigate) {
    const userRecord = JSON.parse(localStorage.getItem("capstone_user"));
    if (userRecord.type === "google") {
      GoogleAuth.signOut(navigate);
    } else if (userRecord.type === "email") {
      emailAuth.signOut(navigate);
    }
  },
};
