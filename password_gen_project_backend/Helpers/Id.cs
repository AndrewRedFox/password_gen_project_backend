namespace password_gen_project_backend.Helpers
{
    public static class Id
    {
        private static int id = 20;

        public static int get()
        {
            id += 1;
            return id;
        }
    }
}
