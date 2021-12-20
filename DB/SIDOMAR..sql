--
-- PostgreSQL database dump
--

-- Dumped from database version 13.2
-- Dumped by pg_dump version 13.2

-- Started on 2021-12-20 10:42:17

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

--
-- TOC entry 4 (class 2615 OID 2200)
-- Name: public; Type: SCHEMA; Schema: -; Owner: postgres
--

CREATE SCHEMA public;


ALTER SCHEMA public OWNER TO postgres;

--
-- TOC entry 3944 (class 0 OID 0)
-- Dependencies: 4
-- Name: SCHEMA public; Type: COMMENT; Schema: -; Owner: postgres
--

COMMENT ON SCHEMA public IS 'standard public schema';


SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 210 (class 1259 OID 19459)
-- Name: tblkategori; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.tblkategori (
    kategori character varying(100) NOT NULL,
    katstatus boolean,
    katid character varying
);


ALTER TABLE public.tblkategori OWNER TO postgres;

--
-- TOC entry 201 (class 1259 OID 18357)
-- Name: tblproduk; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.tblproduk (
    proid character varying NOT NULL,
    pronama character varying(250),
    prostok integer,
    prohargabeli double precision,
    prohargajual double precision,
    kategori character varying,
    promasuk integer
);


ALTER TABLE public.tblproduk OWNER TO postgres;

--
-- TOC entry 218 (class 1259 OID 19599)
-- Name: qproduk; Type: VIEW; Schema: public; Owner: postgres
--

CREATE VIEW public.qproduk AS
 SELECT tblproduk.proid,
    tblproduk.pronama,
    tblkategori.kategori,
    tblproduk.prostok,
    tblproduk.prohargabeli,
    tblproduk.prohargajual
   FROM (public.tblproduk
     JOIN public.tblkategori ON (((tblproduk.kategori)::text = (tblkategori.kategori)::text)));


ALTER TABLE public.qproduk OWNER TO postgres;

--
-- TOC entry 213 (class 1259 OID 19489)
-- Name: tbldetailtransaksi; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.tbldetailtransaksi (
    detid bigint NOT NULL,
    noinvoice character varying,
    proid character varying,
    jumlahbeli integer,
    totalharga double precision
);


ALTER TABLE public.tbldetailtransaksi OWNER TO postgres;

--
-- TOC entry 209 (class 1259 OID 19436)
-- Name: tblgerai; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.tblgerai (
    geraiid integer NOT NULL,
    lokasi public.geometry(Point,4326),
    notelp character varying(14),
    namagerai character varying(100),
    alamat character varying(250)
);


ALTER TABLE public.tblgerai OWNER TO postgres;

--
-- TOC entry 211 (class 1259 OID 19477)
-- Name: tbltransaksi; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.tbltransaksi (
    noinvoice character varying(250) NOT NULL,
    tgltransaksi timestamp(0) without time zone,
    userid character varying
);


ALTER TABLE public.tbltransaksi OWNER TO postgres;

--
-- TOC entry 202 (class 1259 OID 18370)
-- Name: tbluser; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.tbluser (
    userid character varying(200) NOT NULL,
    dusername character varying(100),
    dpassword character varying(20),
    pemilik_akun character varying(100),
    hakakses character varying,
    geraiid integer
);


ALTER TABLE public.tbluser OWNER TO postgres;

--
-- TOC entry 217 (class 1259 OID 19593)
-- Name: qtopbuy; Type: VIEW; Schema: public; Owner: postgres
--

CREATE VIEW public.qtopbuy AS
 SELECT DISTINCT tblproduk.pronama,
    sum(tbldetailtransaksi.jumlahbeli) AS totalbeli
   FROM ((((public.tblproduk
     JOIN public.tbldetailtransaksi ON (((tblproduk.proid)::text = (tbldetailtransaksi.proid)::text)))
     JOIN public.tbltransaksi ON (((tbltransaksi.noinvoice)::text = (tbldetailtransaksi.noinvoice)::text)))
     JOIN public.tbluser ON (((tbluser.userid)::text = (tbltransaksi.userid)::text)))
     JOIN public.tblgerai ON ((tblgerai.geraiid = tbluser.geraiid)))
  WHERE (tblgerai.geraiid = ( SELECT tbluser_1.geraiid
           FROM public.tbluser tbluser_1
          WHERE ((tbluser_1.dusername)::text = 'admin4'::text)))
  GROUP BY tblproduk.pronama
  ORDER BY (sum(tbldetailtransaksi.jumlahbeli)) DESC;


ALTER TABLE public.qtopbuy OWNER TO postgres;

--
-- TOC entry 212 (class 1259 OID 19487)
-- Name: tbldetailtransaksi_detid_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.tbldetailtransaksi_detid_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.tbldetailtransaksi_detid_seq OWNER TO postgres;

--
-- TOC entry 3945 (class 0 OID 0)
-- Dependencies: 212
-- Name: tbldetailtransaksi_detid_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.tbldetailtransaksi_detid_seq OWNED BY public.tbldetailtransaksi.detid;


--
-- TOC entry 208 (class 1259 OID 19434)
-- Name: tblgerai_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.tblgerai_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.tblgerai_id_seq OWNER TO postgres;

--
-- TOC entry 3946 (class 0 OID 0)
-- Dependencies: 208
-- Name: tblgerai_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.tblgerai_id_seq OWNED BY public.tblgerai.geraiid;


--
-- TOC entry 214 (class 1259 OID 19508)
-- Name: tblhakakses; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.tblhakakses (
    hakakses character varying(100) NOT NULL,
    hakdeskripsi character varying(250)
);


ALTER TABLE public.tblhakakses OWNER TO postgres;

--
-- TOC entry 216 (class 1259 OID 19515)
-- Name: tblmenu; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.tblmenu (
    idmenu integer NOT NULL,
    hakakses character varying,
    menutag integer,
    flag boolean,
    menucaption character varying(100)
);


ALTER TABLE public.tblmenu OWNER TO postgres;

--
-- TOC entry 215 (class 1259 OID 19513)
-- Name: tblmenu_idmenu_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.tblmenu_idmenu_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.tblmenu_idmenu_seq OWNER TO postgres;

--
-- TOC entry 3947 (class 0 OID 0)
-- Dependencies: 215
-- Name: tblmenu_idmenu_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.tblmenu_idmenu_seq OWNED BY public.tblmenu.idmenu;


--
-- TOC entry 3763 (class 2604 OID 19492)
-- Name: tbldetailtransaksi detid; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.tbldetailtransaksi ALTER COLUMN detid SET DEFAULT nextval('public.tbldetailtransaksi_detid_seq'::regclass);


--
-- TOC entry 3762 (class 2604 OID 19439)
-- Name: tblgerai geraiid; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.tblgerai ALTER COLUMN geraiid SET DEFAULT nextval('public.tblgerai_id_seq'::regclass);


--
-- TOC entry 3764 (class 2604 OID 19518)
-- Name: tblmenu idmenu; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.tblmenu ALTER COLUMN idmenu SET DEFAULT nextval('public.tblmenu_idmenu_seq'::regclass);


--
-- TOC entry 3760 (class 0 OID 18696)
-- Dependencies: 204
-- Data for Name: spatial_ref_sys; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 3935 (class 0 OID 19489)
-- Dependencies: 213
-- Data for Name: tbldetailtransaksi; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.tbldetailtransaksi VALUES (21, 'INV20211218190617', 'PRO20211216234042', 10, 35000);
INSERT INTO public.tbldetailtransaksi VALUES (22, 'INV20211218190737', 'PRO20211216234042', 15, 52500);
INSERT INTO public.tbldetailtransaksi VALUES (23, 'INV20211218201543', 'PRO20211216233847', 2, 72000);
INSERT INTO public.tbldetailtransaksi VALUES (24, 'INV20211219101245', 'PRO20211216233847', 3, 108000);
INSERT INTO public.tbldetailtransaksi VALUES (25, 'INV20211219101245', 'PRO20211216234042', 5, 17500);
INSERT INTO public.tbldetailtransaksi VALUES (26, 'INV20211220090105', 'PRO20211220090105', 5, 25000);
INSERT INTO public.tbldetailtransaksi VALUES (27, 'INV20211220090105', 'PRO20211216234042', 5, 17500);
INSERT INTO public.tbldetailtransaksi VALUES (28, 'INV20211220093227', 'PRO20211216233847', 4, 144000);
INSERT INTO public.tbldetailtransaksi VALUES (29, 'INV20211220093247', 'PRO20211216233847', 4, 144000);


--
-- TOC entry 3931 (class 0 OID 19436)
-- Dependencies: 209
-- Data for Name: tblgerai; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.tblgerai VALUES (2, '0101000020E61000002D3C7ED34D2E5C40769EE5D4A8411DC0', '+62816500580', 'Indomaret Ketintang Madya', 'Jl. Ketintang Madya, Ketintang, Kec. Gayungan, Kota SBY, Jawa Timur 60232');
INSERT INTO public.tblgerai VALUES (3, '0101000020E6100000C79D6FFDC72E5C40F8541E2D6C421DC0', '+62816500580', 'Indomaret Ketintang Baru', 'Jl. Ketintang Baru VI, Ketintang, Surabaya, Kota SBY, Jawa Timur 60231');


--
-- TOC entry 3936 (class 0 OID 19508)
-- Dependencies: 214
-- Data for Name: tblhakakses; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.tblhakakses VALUES ('admin', 'admin');
INSERT INTO public.tblhakakses VALUES ('Super Admin', 'Kepala cabang gerai');


--
-- TOC entry 3932 (class 0 OID 19459)
-- Dependencies: 210
-- Data for Name: tblkategori; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.tblkategori VALUES ('Makanan', true, 'KAT20211214145131');
INSERT INTO public.tblkategori VALUES ('Minuman', true, 'KAT20211214151109');


--
-- TOC entry 3938 (class 0 OID 19515)
-- Dependencies: 216
-- Data for Name: tblmenu; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.tblmenu VALUES (11, 'admin', 1, true, 'Dashboard');
INSERT INTO public.tblmenu VALUES (12, 'admin', 2, true, 'Produk');
INSERT INTO public.tblmenu VALUES (13, 'admin', 3, true, 'Transaksi Penjualan');
INSERT INTO public.tblmenu VALUES (14, 'admin', 4, true, 'Gerai');
INSERT INTO public.tblmenu VALUES (15, 'admin', 5, false, 'Hak Akses');
INSERT INTO public.tblmenu VALUES (26, 'Super Admin', 1, true, 'Dashboard');
INSERT INTO public.tblmenu VALUES (27, 'Super Admin', 2, true, 'Produk');
INSERT INTO public.tblmenu VALUES (28, 'Super Admin', 3, true, 'Transaksi Penjualan');
INSERT INTO public.tblmenu VALUES (29, 'Super Admin', 4, true, 'Gerai');
INSERT INTO public.tblmenu VALUES (30, 'Super Admin', 5, true, 'Hak Akses');


--
-- TOC entry 3928 (class 0 OID 18357)
-- Dependencies: 201
-- Data for Name: tblproduk; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.tblproduk VALUES ('PRO20211216233847', 'Koko Krunch', 56, 35000, 36000, 'Makanan', 200);
INSERT INTO public.tblproduk VALUES ('PRO20211216234042', 'Indomie Goreng', -50, 3200, 3500, 'Makanan', 100);
INSERT INTO public.tblproduk VALUES ('PRO20211220090105', 'Pop mie rasa baso', 530, 4500, 5000, 'Makanan', 50);


--
-- TOC entry 3933 (class 0 OID 19477)
-- Dependencies: 211
-- Data for Name: tbltransaksi; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.tbltransaksi VALUES ('INV20211218190617', '2021-12-18 19:06:17', 'USERID20211215113457');
INSERT INTO public.tbltransaksi VALUES ('INV20211218190737', '2021-12-18 19:07:37', 'USERID20211216190440');
INSERT INTO public.tbltransaksi VALUES ('INV20211218201543', '2021-12-18 20:15:43', 'USERID20211216190440');
INSERT INTO public.tbltransaksi VALUES ('INV20211219101245', '2021-12-19 10:12:45', 'USERID20211216183535');
INSERT INTO public.tbltransaksi VALUES ('INV20211220090105', '2021-12-20 09:01:04', 'USERID20211215113457');
INSERT INTO public.tbltransaksi VALUES ('INV20211220093227', '2021-12-20 09:32:27', 'USERID20211215113457');
INSERT INTO public.tbltransaksi VALUES ('INV20211220093247', '2021-12-20 09:32:27', 'USERID20211215113457');


--
-- TOC entry 3929 (class 0 OID 18370)
-- Dependencies: 202
-- Data for Name: tbluser; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.tbluser VALUES ('USERID20211216183725', 'admin3', 'admin3', 'Admin3', 'admin', 3);
INSERT INTO public.tbluser VALUES ('USERID20211216190440', 'admin4', 'admin4', 'admin4', 'admin', 3);
INSERT INTO public.tbluser VALUES ('USERID20211216183535', 'admin1', 'admin1', 'Admin', 'admin', 2);
INSERT INTO public.tbluser VALUES ('USERID20211215113457', 'admin', 'admin', 'Asadel', 'admin', 2);
INSERT INTO public.tbluser VALUES ('USERID20211220091154', 'james', 'james123', 'James', 'admin', 2);
INSERT INTO public.tbluser VALUES ('USERID20211220092412', 'sasaki', 'sasaki123', 'Sasaki', 'admin', 2);


--
-- TOC entry 3948 (class 0 OID 0)
-- Dependencies: 212
-- Name: tbldetailtransaksi_detid_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.tbldetailtransaksi_detid_seq', 29, true);


--
-- TOC entry 3949 (class 0 OID 0)
-- Dependencies: 208
-- Name: tblgerai_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.tblgerai_id_seq', 3, true);


--
-- TOC entry 3950 (class 0 OID 0)
-- Dependencies: 215
-- Name: tblmenu_idmenu_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.tblmenu_idmenu_seq', 30, true);


--
-- TOC entry 3779 (class 2606 OID 19497)
-- Name: tbldetailtransaksi tbldetailtransaksi_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.tbldetailtransaksi
    ADD CONSTRAINT tbldetailtransaksi_pkey PRIMARY KEY (detid);


--
-- TOC entry 3773 (class 2606 OID 19441)
-- Name: tblgerai tblgerai_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.tblgerai
    ADD CONSTRAINT tblgerai_pkey PRIMARY KEY (geraiid);


--
-- TOC entry 3781 (class 2606 OID 19512)
-- Name: tblhakakses tblhakakses_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.tblhakakses
    ADD CONSTRAINT tblhakakses_pkey PRIMARY KEY (hakakses);


--
-- TOC entry 3775 (class 2606 OID 19466)
-- Name: tblkategori tblkategori_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.tblkategori
    ADD CONSTRAINT tblkategori_pkey PRIMARY KEY (kategori);


--
-- TOC entry 3783 (class 2606 OID 19523)
-- Name: tblmenu tblmenu_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.tblmenu
    ADD CONSTRAINT tblmenu_pkey PRIMARY KEY (idmenu);


--
-- TOC entry 3766 (class 2606 OID 18364)
-- Name: tblproduk tblproduk_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.tblproduk
    ADD CONSTRAINT tblproduk_pkey PRIMARY KEY (proid);


--
-- TOC entry 3777 (class 2606 OID 19481)
-- Name: tbltransaksi tbltransaksi_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.tbltransaksi
    ADD CONSTRAINT tbltransaksi_pkey PRIMARY KEY (noinvoice);


--
-- TOC entry 3768 (class 2606 OID 19533)
-- Name: tbluser tbluser_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.tbluser
    ADD CONSTRAINT tbluser_pkey PRIMARY KEY (userid);


--
-- TOC entry 3771 (class 1259 OID 19445)
-- Name: sidx_tblgerai_lokasi; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX sidx_tblgerai_lokasi ON public.tblgerai USING gist (lokasi);


--
-- TOC entry 3786 (class 2606 OID 19567)
-- Name: tbluser geraiid; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.tbluser
    ADD CONSTRAINT geraiid FOREIGN KEY (geraiid) REFERENCES public.tblgerai(geraiid) ON UPDATE CASCADE NOT VALID;


--
-- TOC entry 3790 (class 2606 OID 19524)
-- Name: tblmenu hakakses; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.tblmenu
    ADD CONSTRAINT hakakses FOREIGN KEY (hakakses) REFERENCES public.tblhakakses(hakakses) ON UPDATE CASCADE;


--
-- TOC entry 3785 (class 2606 OID 19534)
-- Name: tbluser hakakses; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.tbluser
    ADD CONSTRAINT hakakses FOREIGN KEY (hakakses) REFERENCES public.tblhakakses(hakakses) ON UPDATE CASCADE NOT VALID;


--
-- TOC entry 3784 (class 2606 OID 19467)
-- Name: tblproduk kategori; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.tblproduk
    ADD CONSTRAINT kategori FOREIGN KEY (kategori) REFERENCES public.tblkategori(kategori) ON UPDATE CASCADE NOT VALID;


--
-- TOC entry 3788 (class 2606 OID 19498)
-- Name: tbldetailtransaksi noinvoice; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.tbldetailtransaksi
    ADD CONSTRAINT noinvoice FOREIGN KEY (noinvoice) REFERENCES public.tbltransaksi(noinvoice) ON UPDATE CASCADE;


--
-- TOC entry 3789 (class 2606 OID 19503)
-- Name: tbldetailtransaksi proid; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.tbldetailtransaksi
    ADD CONSTRAINT proid FOREIGN KEY (proid) REFERENCES public.tblproduk(proid) ON UPDATE CASCADE;


--
-- TOC entry 3787 (class 2606 OID 19580)
-- Name: tbltransaksi userid; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.tbltransaksi
    ADD CONSTRAINT userid FOREIGN KEY (userid) REFERENCES public.tbluser(userid) ON UPDATE CASCADE NOT VALID;


-- Completed on 2021-12-20 10:42:17

--
-- PostgreSQL database dump complete
--

