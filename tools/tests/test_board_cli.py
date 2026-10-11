import unittest

from tools.board.__main__ import parse_args


class BoardCliTest(unittest.TestCase):
    def test_por_defecto_solo_escucha_en_local(self):
        args = parse_args([])
        self.assertEqual(args.host, "127.0.0.1")
        self.assertEqual(args.port, 8765)

    def test_docker_escucha_en_todas_las_interfaces(self):
        args = parse_args(["--host", "0.0.0.0", "--no-browser", "--no-git"])
        self.assertEqual(args.host, "0.0.0.0")
        self.assertTrue(args.no_browser and args.no_git)


if __name__ == "__main__":
    unittest.main()
