

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;


COMMENT ON SCHEMA "public" IS 'standard public schema';



CREATE EXTENSION IF NOT EXISTS "pg_graphql" WITH SCHEMA "graphql";






CREATE EXTENSION IF NOT EXISTS "pg_stat_statements" WITH SCHEMA "extensions";






CREATE EXTENSION IF NOT EXISTS "pgcrypto" WITH SCHEMA "extensions";






CREATE EXTENSION IF NOT EXISTS "pgjwt" WITH SCHEMA "extensions";






CREATE EXTENSION IF NOT EXISTS "supabase_vault" WITH SCHEMA "vault";






CREATE EXTENSION IF NOT EXISTS "uuid-ossp" WITH SCHEMA "extensions";





SET default_tablespace = '';

SET default_table_access_method = "heap";


CREATE TABLE IF NOT EXISTS "public"."pokemon_card_instances" (
    "id" "uuid" DEFAULT "gen_random_uuid"() NOT NULL,
    "card_id" "uuid" NOT NULL,
    "current_owner" "uuid",
    "grade" numeric,
    "grading_company" "text",
    "notes" "text" DEFAULT ''::"text" NOT NULL,
    "created_at" timestamp with time zone DEFAULT ("now"() AT TIME ZONE 'utc'::"text") NOT NULL
);


ALTER TABLE "public"."pokemon_card_instances" OWNER TO "postgres";


CREATE TABLE IF NOT EXISTS "public"."pokemon_cards" (
    "uuid" "uuid" DEFAULT "gen_random_uuid"() NOT NULL,
    "set" "text" NOT NULL,
    "card_number" "text" NOT NULL,
    "name" "text" NOT NULL,
    "type" "text" NOT NULL,
    "form" "jsonb",
    "image_url" "text",
    "rarity" "text",
    "pokedex_number" bigint,
    "typings" "jsonb"
);


ALTER TABLE "public"."pokemon_cards" OWNER TO "postgres";


CREATE TABLE IF NOT EXISTS "public"."pokemon_collections" (
    "id" "uuid" DEFAULT "gen_random_uuid"() NOT NULL,
    "user_id" "uuid",
    "name" "text" DEFAULT ''::"text",
    "description" "text" DEFAULT ''::"text",
    "is_public" boolean,
    "created_at" timestamp with time zone DEFAULT "now"()
);


ALTER TABLE "public"."pokemon_collections" OWNER TO "postgres";


CREATE TABLE IF NOT EXISTS "public"."pokemon_collections_cards" (
    "collection_id" "uuid" NOT NULL,
    "instance_id" "uuid" NOT NULL
);


ALTER TABLE "public"."pokemon_collections_cards" OWNER TO "postgres";


CREATE TABLE IF NOT EXISTS "public"."pokemon_decks" (
    "id" "uuid" DEFAULT "gen_random_uuid"() NOT NULL,
    "user_id" "uuid",
    "name" "text" DEFAULT ''::"text",
    "description" "text" DEFAULT ''::"text",
    "is_public" boolean DEFAULT true,
    "created_at" timestamp with time zone DEFAULT ("now"() AT TIME ZONE 'utc'::"text")
);


ALTER TABLE "public"."pokemon_decks" OWNER TO "postgres";


CREATE TABLE IF NOT EXISTS "public"."pokemon_decks_cards" (
    "deck_id" "uuid" NOT NULL,
    "instance_id" "uuid" NOT NULL
);


ALTER TABLE "public"."pokemon_decks_cards" OWNER TO "postgres";


CREATE TABLE IF NOT EXISTS "public"."pokemon_sets" (
    "name" "text" NOT NULL,
    "code" "text",
    "total_cards" bigint,
    "release_date" "date"
);


ALTER TABLE "public"."pokemon_sets" OWNER TO "postgres";


CREATE TABLE IF NOT EXISTS "public"."pokemon_traded_cards" (
    "id" "uuid" DEFAULT "gen_random_uuid"() NOT NULL,
    "trade_id" "uuid",
    "instance_id" "uuid",
    "grade" numeric,
    "grading_company" "text" DEFAULT ''::"text",
    "notes" "text" DEFAULT ''::"text"
);


ALTER TABLE "public"."pokemon_traded_cards" OWNER TO "postgres";


CREATE TABLE IF NOT EXISTS "public"."pokemon_trades" (
    "id" "uuid" DEFAULT "gen_random_uuid"() NOT NULL,
    "from_user_id" "uuid",
    "to_user_id" "uuid",
    "created_at" timestamp with time zone DEFAULT ("now"() AT TIME ZONE 'utc'::"text"),
    "trade_type" "text" DEFAULT ''::"text",
    "price" numeric,
    "traded_in" "jsonb",
    "traded_for" "jsonb",
    "state" "text" DEFAULT ''::"text",
    "finalized_at" timestamp with time zone DEFAULT ("now"() AT TIME ZONE 'utc'::"text")
);


ALTER TABLE "public"."pokemon_trades" OWNER TO "postgres";


CREATE TABLE IF NOT EXISTS "public"."users" (
    "id" "uuid" DEFAULT "gen_random_uuid"() NOT NULL,
    "username" "text" NOT NULL,
    "password_hash" "text",
    "created_at" timestamp with time zone DEFAULT ("now"() AT TIME ZONE 'utc'::"text") NOT NULL,
    "last_username" "text",
    "bio" "text" DEFAULT ''::"text" NOT NULL
);


ALTER TABLE "public"."users" OWNER TO "postgres";


ALTER TABLE ONLY "public"."pokemon_cards"
    ADD CONSTRAINT "pokemon-cards_duplicate_pkey" PRIMARY KEY ("uuid");



ALTER TABLE ONLY "public"."pokemon_card_instances"
    ADD CONSTRAINT "pokemon_card_instances_pkey" PRIMARY KEY ("id");



ALTER TABLE ONLY "public"."pokemon_collections_cards"
    ADD CONSTRAINT "pokemon_collections_cards_pkey" PRIMARY KEY ("collection_id", "instance_id");



ALTER TABLE ONLY "public"."pokemon_collections"
    ADD CONSTRAINT "pokemon_collections_pkey" PRIMARY KEY ("id");



ALTER TABLE ONLY "public"."pokemon_decks_cards"
    ADD CONSTRAINT "pokemon_decks_cards_pkey" PRIMARY KEY ("deck_id", "instance_id");



ALTER TABLE ONLY "public"."pokemon_decks"
    ADD CONSTRAINT "pokemon_decks_pkey" PRIMARY KEY ("id");



ALTER TABLE ONLY "public"."pokemon_sets"
    ADD CONSTRAINT "pokemon_sets_pkey" PRIMARY KEY ("name");



ALTER TABLE ONLY "public"."pokemon_traded_cards"
    ADD CONSTRAINT "pokemon_traded_cards_pkey" PRIMARY KEY ("id");



ALTER TABLE ONLY "public"."pokemon_trades"
    ADD CONSTRAINT "pokemon_trades_pkey" PRIMARY KEY ("id");



ALTER TABLE ONLY "public"."users"
    ADD CONSTRAINT "users_pkey" PRIMARY KEY ("id");



ALTER TABLE ONLY "public"."users"
    ADD CONSTRAINT "users_username_key" UNIQUE ("username");



ALTER TABLE ONLY "public"."pokemon_card_instances"
    ADD CONSTRAINT "pokemon_card_instances_card_id_fkey" FOREIGN KEY ("card_id") REFERENCES "public"."pokemon_cards"("uuid") ON UPDATE CASCADE ON DELETE CASCADE;



ALTER TABLE ONLY "public"."pokemon_card_instances"
    ADD CONSTRAINT "pokemon_card_instances_current_owner_fkey" FOREIGN KEY ("current_owner") REFERENCES "public"."users"("id") ON UPDATE CASCADE ON DELETE SET NULL;



ALTER TABLE ONLY "public"."pokemon_collections_cards"
    ADD CONSTRAINT "pokemon_collections_cards_collection_id_fkey" FOREIGN KEY ("collection_id") REFERENCES "public"."pokemon_collections"("id") ON UPDATE CASCADE ON DELETE CASCADE;



ALTER TABLE ONLY "public"."pokemon_collections_cards"
    ADD CONSTRAINT "pokemon_collections_cards_instance_id_fkey" FOREIGN KEY ("instance_id") REFERENCES "public"."pokemon_card_instances"("id") ON UPDATE CASCADE ON DELETE CASCADE;



ALTER TABLE ONLY "public"."pokemon_collections"
    ADD CONSTRAINT "pokemon_collections_user_id_fkey" FOREIGN KEY ("user_id") REFERENCES "public"."users"("id") ON UPDATE CASCADE;



ALTER TABLE ONLY "public"."pokemon_decks_cards"
    ADD CONSTRAINT "pokemon_decks_cards_deck_id_fkey" FOREIGN KEY ("deck_id") REFERENCES "public"."pokemon_decks"("id") ON UPDATE CASCADE ON DELETE CASCADE;



ALTER TABLE ONLY "public"."pokemon_decks_cards"
    ADD CONSTRAINT "pokemon_decks_cards_instance_id_fkey" FOREIGN KEY ("instance_id") REFERENCES "public"."pokemon_card_instances"("id") ON UPDATE CASCADE ON DELETE CASCADE;



ALTER TABLE ONLY "public"."pokemon_decks"
    ADD CONSTRAINT "pokemon_decks_user_id_fkey" FOREIGN KEY ("user_id") REFERENCES "public"."users"("id") ON UPDATE CASCADE ON DELETE CASCADE;



ALTER TABLE ONLY "public"."pokemon_traded_cards"
    ADD CONSTRAINT "pokemon_traded_cards_instance_id_fkey" FOREIGN KEY ("instance_id") REFERENCES "public"."pokemon_card_instances"("id") ON UPDATE CASCADE ON DELETE CASCADE;



ALTER TABLE ONLY "public"."pokemon_traded_cards"
    ADD CONSTRAINT "pokemon_traded_cards_trade_id_fkey" FOREIGN KEY ("trade_id") REFERENCES "public"."pokemon_trades"("id") ON UPDATE CASCADE ON DELETE CASCADE;



ALTER TABLE ONLY "public"."pokemon_trades"
    ADD CONSTRAINT "pokemon_trades_from_user_id_fkey" FOREIGN KEY ("from_user_id") REFERENCES "public"."users"("id") ON UPDATE CASCADE ON DELETE CASCADE;



ALTER TABLE ONLY "public"."pokemon_trades"
    ADD CONSTRAINT "pokemon_trades_to_user_id_fkey" FOREIGN KEY ("to_user_id") REFERENCES "public"."users"("id") ON UPDATE CASCADE ON DELETE CASCADE;



ALTER TABLE "public"."pokemon_card_instances" ENABLE ROW LEVEL SECURITY;


ALTER TABLE "public"."pokemon_cards" ENABLE ROW LEVEL SECURITY;


ALTER TABLE "public"."pokemon_collections" ENABLE ROW LEVEL SECURITY;


ALTER TABLE "public"."pokemon_collections_cards" ENABLE ROW LEVEL SECURITY;


ALTER TABLE "public"."pokemon_decks" ENABLE ROW LEVEL SECURITY;


ALTER TABLE "public"."pokemon_decks_cards" ENABLE ROW LEVEL SECURITY;


ALTER TABLE "public"."pokemon_sets" ENABLE ROW LEVEL SECURITY;


ALTER TABLE "public"."pokemon_traded_cards" ENABLE ROW LEVEL SECURITY;


ALTER TABLE "public"."pokemon_trades" ENABLE ROW LEVEL SECURITY;


ALTER TABLE "public"."users" ENABLE ROW LEVEL SECURITY;




ALTER PUBLICATION "supabase_realtime" OWNER TO "postgres";


GRANT USAGE ON SCHEMA "public" TO "postgres";
GRANT USAGE ON SCHEMA "public" TO "anon";
GRANT USAGE ON SCHEMA "public" TO "authenticated";
GRANT USAGE ON SCHEMA "public" TO "service_role";


























































































































































































GRANT ALL ON TABLE "public"."pokemon_card_instances" TO "anon";
GRANT ALL ON TABLE "public"."pokemon_card_instances" TO "authenticated";
GRANT ALL ON TABLE "public"."pokemon_card_instances" TO "service_role";



GRANT ALL ON TABLE "public"."pokemon_cards" TO "anon";
GRANT ALL ON TABLE "public"."pokemon_cards" TO "authenticated";
GRANT ALL ON TABLE "public"."pokemon_cards" TO "service_role";



GRANT ALL ON TABLE "public"."pokemon_collections" TO "anon";
GRANT ALL ON TABLE "public"."pokemon_collections" TO "authenticated";
GRANT ALL ON TABLE "public"."pokemon_collections" TO "service_role";



GRANT ALL ON TABLE "public"."pokemon_collections_cards" TO "anon";
GRANT ALL ON TABLE "public"."pokemon_collections_cards" TO "authenticated";
GRANT ALL ON TABLE "public"."pokemon_collections_cards" TO "service_role";



GRANT ALL ON TABLE "public"."pokemon_decks" TO "anon";
GRANT ALL ON TABLE "public"."pokemon_decks" TO "authenticated";
GRANT ALL ON TABLE "public"."pokemon_decks" TO "service_role";



GRANT ALL ON TABLE "public"."pokemon_decks_cards" TO "anon";
GRANT ALL ON TABLE "public"."pokemon_decks_cards" TO "authenticated";
GRANT ALL ON TABLE "public"."pokemon_decks_cards" TO "service_role";



GRANT ALL ON TABLE "public"."pokemon_sets" TO "anon";
GRANT ALL ON TABLE "public"."pokemon_sets" TO "authenticated";
GRANT ALL ON TABLE "public"."pokemon_sets" TO "service_role";



GRANT ALL ON TABLE "public"."pokemon_traded_cards" TO "anon";
GRANT ALL ON TABLE "public"."pokemon_traded_cards" TO "authenticated";
GRANT ALL ON TABLE "public"."pokemon_traded_cards" TO "service_role";



GRANT ALL ON TABLE "public"."pokemon_trades" TO "anon";
GRANT ALL ON TABLE "public"."pokemon_trades" TO "authenticated";
GRANT ALL ON TABLE "public"."pokemon_trades" TO "service_role";



GRANT ALL ON TABLE "public"."users" TO "anon";
GRANT ALL ON TABLE "public"."users" TO "authenticated";
GRANT ALL ON TABLE "public"."users" TO "service_role";









ALTER DEFAULT PRIVILEGES FOR ROLE "postgres" IN SCHEMA "public" GRANT ALL ON SEQUENCES  TO "postgres";
ALTER DEFAULT PRIVILEGES FOR ROLE "postgres" IN SCHEMA "public" GRANT ALL ON SEQUENCES  TO "anon";
ALTER DEFAULT PRIVILEGES FOR ROLE "postgres" IN SCHEMA "public" GRANT ALL ON SEQUENCES  TO "authenticated";
ALTER DEFAULT PRIVILEGES FOR ROLE "postgres" IN SCHEMA "public" GRANT ALL ON SEQUENCES  TO "service_role";






ALTER DEFAULT PRIVILEGES FOR ROLE "postgres" IN SCHEMA "public" GRANT ALL ON FUNCTIONS  TO "postgres";
ALTER DEFAULT PRIVILEGES FOR ROLE "postgres" IN SCHEMA "public" GRANT ALL ON FUNCTIONS  TO "anon";
ALTER DEFAULT PRIVILEGES FOR ROLE "postgres" IN SCHEMA "public" GRANT ALL ON FUNCTIONS  TO "authenticated";
ALTER DEFAULT PRIVILEGES FOR ROLE "postgres" IN SCHEMA "public" GRANT ALL ON FUNCTIONS  TO "service_role";






ALTER DEFAULT PRIVILEGES FOR ROLE "postgres" IN SCHEMA "public" GRANT ALL ON TABLES  TO "postgres";
ALTER DEFAULT PRIVILEGES FOR ROLE "postgres" IN SCHEMA "public" GRANT ALL ON TABLES  TO "anon";
ALTER DEFAULT PRIVILEGES FOR ROLE "postgres" IN SCHEMA "public" GRANT ALL ON TABLES  TO "authenticated";
ALTER DEFAULT PRIVILEGES FOR ROLE "postgres" IN SCHEMA "public" GRANT ALL ON TABLES  TO "service_role";






























RESET ALL;
