import { z } from "zod";

export const loginSchema = z.object({
  username: z
    .string()
    .trim()
    .nonempty("Username is required")
    .max(128, "Username is too long"),

  password: z
    .string()
    .trim()
    .nonempty("Password is required")
    .min(6, "Invalid password, min length 6 digits"),
});
