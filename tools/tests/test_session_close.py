import unittest

from tools.session.close import history_row


class HistoryRowTests(unittest.TestCase):
    def test_row_links_pr_and_report_with_spanish_decimal(self):
        row = history_row(3, "F1-01", 2, 11, 73, "2026-10-11")
        self.assertEqual(
            row,
            "| 3 | F1-01 | 2026-10-11 | [#2](https://github.com/MenaDeFerro/Project-Especimen/pull/2) | "
            "11/73 · 15,1 % | [SESION-03](SESION-03.md) |")


if __name__ == "__main__":
    unittest.main()
