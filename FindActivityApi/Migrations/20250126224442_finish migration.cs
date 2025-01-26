using Microsoft.EntityFrameworkCore.Migrations;

namespace FindActivityApi.Migrations
{
    /// <inheritdoc />
    public partial class finishmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE OR REPLACE PROCEDURE public.addcategorywithactivities(
                    IN new_category_name character varying,
                    IN activities_list text[]
                )
                LANGUAGE plpgsql
                AS $procedure$
                DECLARE
                    new_category_id INT;
                BEGIN
                    INSERT INTO ""Categories"" (""CategoryName"")
                    VALUES (new_category_name)
                    RETURNING ""CategoryId"" INTO new_category_id;

                    INSERT INTO ""Activities"" (""ActivityName"", ""CategoryId"")
                    SELECT activity, new_category_id
                    FROM unnest(activities_list) AS activity;
                END;
                $procedure$
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP PROCEDURE IF EXISTS public.addcategorywithactivities");
        }
    }
}

